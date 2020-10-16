using System;
using Android.Graphics.Drawables;
using AndroidX.Core.View;
using AndroidX.Core.Widget;
using Xamarin.Forms;
using AButton = Android.Widget.Button;
using ARect = Android.Graphics.Rect;
using AView = Android.Views.View;

namespace Xamarin.Platform
{
	// TODO: Currently the drawable is reloaded if the text or the layout changes.
	//       This is obviously not great, but it works. An optimization should
	//       be made to find the drawable in the view and just re-position.
	//       If we do this, we must remember to undo the offset in OnLayout.
	public class ButtonLayoutManager : IDisposable
	{
		// We use left/right as this does not resize the button when there is no text
		readonly ButtonContentLayout _imageOnlyLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Left, 0);

		// Reuse this instance to save on allocations
		readonly ARect _drawableBounds = new ARect();

		bool _disposed;

		readonly AButton _nativeView;
		readonly IButton? _virtualView;

		Thickness? _defaultPaddingPix;

		readonly bool _alignIconWithText;
		readonly bool _preserveInitialPadding;
		readonly bool _borderAdjustsPadding;
		readonly bool _maintainLegacyMeasurements;

		bool _hasLayoutOccurred;

		public ButtonLayoutManager(AButton renderer, IButton virtualView)
			: this(renderer, virtualView, false, false, false, true)
		{
		}

		public ButtonLayoutManager(AButton nativeView,
			IButton virtualView,
			bool alignIconWithText,
			bool preserveInitialPadding,
			bool borderAdjustsPadding,
			bool maintainLegacyMeasurements)
		{
			_nativeView = nativeView ?? throw new ArgumentNullException(nameof(nativeView));
			_virtualView = virtualView ?? throw new ArgumentNullException(nameof(virtualView));

			_alignIconWithText = alignIconWithText;
			_preserveInitialPadding = preserveInitialPadding;
			_borderAdjustsPadding = borderAdjustsPadding;
			_maintainLegacyMeasurements = maintainLegacyMeasurements;
		}

		public void Dispose()
		{
			if (!_disposed)
			{
				if (_nativeView != null)
					_nativeView.Dispose();
				
				_disposed = true;
			}
		}

		internal SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
		{
			var previousHeight = _nativeView.MeasuredHeight;
			var previousWidth = _nativeView.MeasuredWidth;

			_nativeView.Measure(widthConstraint, heightConstraint);

			// if the measure of the view has changed then trigger a request for layout
			// if the measure hasn't changed then force a layout of the button
			if (previousHeight != _nativeView.MeasuredHeight || previousWidth != _nativeView.MeasuredWidth)
				_nativeView.RequestLayout();
			else
				_nativeView.ForceLayout();

			return new SizeRequest(new Size(_nativeView.MeasuredWidth, _nativeView.MeasuredHeight), Size.Zero);
		}

		public void OnLayout(bool changed, int left, int top, int right, int bottom)
		{
			if (_disposed || _nativeView == null || _virtualView == null)
				return;

			AButton nativeButton = _nativeView;

			if (nativeButton == null)
				return;

			Drawable? drawable = null;
			Drawable[] drawables = TextViewCompat.GetCompoundDrawablesRelative(nativeButton);
			if (drawables != null)
			{
				foreach (var compoundDrawable in drawables)
				{
					if (compoundDrawable != null)
					{
						drawable = compoundDrawable;
						break;
					}
				}
			}

			if (drawable != null)
			{
				int iconWidth = drawable.IntrinsicWidth;
				drawable.CopyBounds(_drawableBounds);

				// Center the drawable in the button if there is no text.
				// We do not need to undo this as when we get some text, the drawable recreated
				if (string.IsNullOrEmpty(_virtualView.Text))
				{
					var newLeft = (right - left - iconWidth) / 2 - nativeButton.PaddingLeft;

					_drawableBounds.Set(newLeft, _drawableBounds.Top, newLeft + iconWidth, _drawableBounds.Bottom);
					drawable.Bounds = _drawableBounds;
				}
				else
				{
					if (_alignIconWithText && _virtualView.ContentLayout.IsHorizontal())
					{
						var buttonText = nativeButton.TextFormatted;

						// if text is transformed, add that transformation to to ensure correct calculation of icon padding
						if (nativeButton.TransformationMethod != null)
							buttonText = nativeButton.TransformationMethod.GetTransformationFormatted(buttonText, nativeButton);

						var measuredTextWidth = (int?)nativeButton.Paint?.MeasureText(buttonText, 0, buttonText != null ? buttonText.Length() : 0);
						var textWidth = Math.Min(measuredTextWidth ?? 0, nativeButton.Layout != null ? nativeButton.Layout.Width : 0);
						var contentsWidth = ViewCompat.GetPaddingStart(nativeButton) + iconWidth + nativeButton.CompoundDrawablePadding + textWidth + ViewCompat.GetPaddingEnd(nativeButton);

						var newLeft = (nativeButton.MeasuredWidth - contentsWidth) / 2;

						if (_virtualView.ContentLayout.Position == ButtonContentLayout.ImagePosition.Right)
							newLeft = -newLeft;

						if (ViewCompat.GetLayoutDirection(nativeButton) == ViewCompat.LayoutDirectionRtl)
							newLeft = -newLeft;

						_drawableBounds.Set(newLeft, _drawableBounds.Top, newLeft + iconWidth, _drawableBounds.Bottom);
						drawable.Bounds = _drawableBounds;
					}
				}
			}

			_hasLayoutOccurred = true;
		}

		public void OnViewAttachedToWindow(AView? attachedView)
		{
			UpdateLayout();
		}

		public void OnViewDetachedFromWindow(AView? detachedView)
		{

		}

		public void UpdateLayout()
		{
			if (_nativeView?.LayoutParameters == null && _hasLayoutOccurred)
				return;

			UpdateTextAndImage();
			UpdatePadding();
			UpdateLineBreakMode();
		}

		void UpdatePadding()
		{
			AButton nativeButton = _nativeView;

			if (nativeButton == null)
				return;

			if (_disposed || _nativeView == null || _nativeView.Context == null || _virtualView == null)
				return;

			if (!_defaultPaddingPix.HasValue)
				_defaultPaddingPix = new Thickness(nativeButton.PaddingLeft, nativeButton.PaddingTop, nativeButton.PaddingRight, nativeButton.PaddingBottom);

			// Currently the Padding property uses a creator factory so once it is set it can't become unset
			// I would say this is currently a bug but it's a bug that exists already in the code base.
			// Having this comment and this code more accurately demonstrates behavior then
			// having an else clause for when the PaddingProperty isn't set
			if (_virtualView.Padding.IsEmpty)
				return;

			var padding = _virtualView.Padding;
			var adjustment = 0.0;

			if (_borderAdjustsPadding && _virtualView is IBorder borderElement && borderElement.BorderWidth != 0)
				adjustment = borderElement.BorderWidth;

			var defaultPadding = _preserveInitialPadding && _defaultPaddingPix.HasValue
				? _defaultPaddingPix.Value
				: new Thickness();

			nativeButton.SetPadding(
				(int)(_nativeView.Context.ToPixels(padding.Left + adjustment) + defaultPadding.Left),
				(int)(_nativeView.Context.ToPixels(padding.Top + adjustment) + defaultPadding.Top),
				(int)(_nativeView.Context.ToPixels(padding.Right + adjustment) + defaultPadding.Right),
				(int)(_nativeView.Context.ToPixels(padding.Bottom + adjustment) + defaultPadding.Bottom));
		}

		bool UpdateTextAndImage()
		{
			if (_disposed || _nativeView == null || _virtualView == null)
				return false;

			if (_nativeView?.LayoutParameters == null && _hasLayoutOccurred)
				return false;

			AButton? nativeButton = _nativeView;

			if (nativeButton == null)
				return false;

			var textTransform = _virtualView?.TextTransform;

			nativeButton.SetAllCaps(textTransform == TextTransform.Default);

			string? oldText = nativeButton.Text;

			if (textTransform.HasValue)
				nativeButton.Text = _virtualView?.UpdateTransformedText(_virtualView.Text, textTransform.Value);

			// If we went from or to having no text, we need to update the image position
			if (string.IsNullOrEmpty(oldText) != string.IsNullOrEmpty(nativeButton.Text))
			{
				return true;
			}

			return false;
		}

		void UpdateLineBreakMode()
		{
			AButton? nativeButton = _nativeView;

			if (nativeButton == null || _virtualView == null || _nativeView == null)
				return;

			nativeButton.SetLineBreakMode(_virtualView);
			nativeButton.SetAllCaps(_virtualView.TextTransform == TextTransform.Default);
		}
	}
}