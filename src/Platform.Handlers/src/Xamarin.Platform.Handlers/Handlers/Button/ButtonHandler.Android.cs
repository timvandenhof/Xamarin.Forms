using AndroidX.AppCompat.Widget;
using Xamarin.Forms;
using AView = Android.Views.View;

namespace Xamarin.Platform.Handlers
{
	public partial class ButtonHandler : AbstractViewHandler<IButton, AppCompatButton>
	{
		static ButtonBorderBackgroundManager? BackgroundTracker { get; set; }
		static ButtonLayoutManager? ButtonLayoutManager { get; set; }

		ButtonAttachStateChangeListener AttachStateChangeListener { get; } = new ButtonAttachStateChangeListener();
		ButtonClickListener ClickListener { get; } = new ButtonClickListener();
		ButtonTouchListener TouchListener { get; } = new ButtonTouchListener();

		protected override AppCompatButton CreateView()
		{
			AppCompatButton nativeButton = new AppCompatButton(Context)
			{
				SoundEffectsEnabled = false
			};

			nativeButton.AddOnAttachStateChangeListener(AttachStateChangeListener);

			ClickListener.Handler = this;
			nativeButton.SetOnClickListener(ClickListener);

			TouchListener.Handler = this;
			nativeButton.SetOnTouchListener(TouchListener);

			return nativeButton;
		}

		public override void TearDown()
		{
			TypedNativeView?.RemoveOnAttachStateChangeListener(AttachStateChangeListener);

			ClickListener.Handler = null;
			TypedNativeView?.SetOnClickListener(null);

			TouchListener.Handler = null;
			TypedNativeView?.SetOnTouchListener(null);

			BackgroundTracker?.Dispose();
			BackgroundTracker = null;

			ButtonLayoutManager?.Dispose();
			ButtonLayoutManager = null;

			base.TearDown();
		}

		public override SizeRequest GetDesiredSize(double wConstraint, double hConstraint)
		{
			var hint = TypedNativeView?.Hint;
			bool setHint = TypedNativeView?.LayoutParameters != null;

			if (TypedNativeView != null && !string.IsNullOrWhiteSpace(hint) && setHint)
			{
				TypedNativeView.Hint = string.Empty;
			}

			int widthConstraint = wConstraint == double.MaxValue ? int.MaxValue : (int)wConstraint;
			int heightConstraint = hConstraint == double.MaxValue ? int.MaxValue : (int)hConstraint;

			TypedNativeView?.Measure(widthConstraint, heightConstraint);

			if (TypedNativeView != null && setHint)
				TypedNativeView.Hint = hint;

			var previousHeight = TypedNativeView?.MeasuredHeight;
			var previousWidth = TypedNativeView?.MeasuredWidth;

			if (View != null)
			{
				// If the measure of the view has changed then trigger a request for layout
				// If the measure hasn't changed then force a layout of the button
				if (previousHeight != View.MeasuredHeight || previousWidth != View.MeasuredWidth)
					View.RequestLayout();
				else
					View.ForceLayout();

				var result = new SizeRequest(new Size(View.MeasuredWidth, View.MeasuredHeight), Size.Zero);

				return result;
			}

			return base.GetDesiredSize(widthConstraint, heightConstraint);
		}

		public static void MapBackgroundColor(ButtonHandler handler, IButton button)
		{
			ViewHandler.CheckParameters(handler, button);
			handler.TypedNativeView?.UpdateBackgroundColor(button);
		}

		public static void MapColor(ButtonHandler handler, IButton button)
		{
			ViewHandler.CheckParameters(handler, button);
			handler.TypedNativeView?.UpdateColor(button);
		}

		public static void MapText(ButtonHandler handler, IButton button)
		{
			ViewHandler.CheckParameters(handler, button);
			handler.TypedNativeView?.UpdateText(button);
		}

		public static void MapFont(ButtonHandler handler, IButton button)
		{
			ViewHandler.CheckParameters(handler, button);
			handler.TypedNativeView?.UpdateFont(button);
		}

		public static void MapCharacterSpacing(ButtonHandler handler, IButton button)
		{
			ViewHandler.CheckParameters(handler, button);
			handler.TypedNativeView?.UpdateCharacterSpacing(button);
		}

		public static void MapCornerRadius(ButtonHandler handler, IButton button)
		{
			ViewHandler.CheckParameters(handler, button);
			handler.TypedNativeView?.UpdateCornerRadius(button, BackgroundTracker);
		}

		public static void MapBorderColor(ButtonHandler handler, IButton button)
		{
			ViewHandler.CheckParameters(handler, button);
			handler.TypedNativeView?.UpdateBorderColor(button, BackgroundTracker);
		}

		public static void MapBorderWidth(ButtonHandler handler, IButton button)
		{
			ViewHandler.CheckParameters(handler, button);
			handler.TypedNativeView?.UpdateBorderWidth(button, BackgroundTracker);
		}

		public static void MapContentLayout(ButtonHandler handler, IButton button)
		{
			ViewHandler.CheckParameters(handler, button);
			handler.TypedNativeView?.UpdateContentLayout(button, ButtonLayoutManager);
		}

		public static void MapPadding(ButtonHandler handler, IButton button)
		{
			ViewHandler.CheckParameters(handler, button);
			handler.TypedNativeView?.UpdatePadding(button, ButtonLayoutManager);
		}

		public class ButtonAttachStateChangeListener : Java.Lang.Object, AView.IOnAttachStateChangeListener
		{
			public void OnViewAttachedToWindow(AView? attachedView)
			{
				ButtonLayoutManager?.OnViewAttachedToWindow(attachedView);
			}

			public void OnViewDetachedFromWindow(AView? detachedView)
			{
				ButtonLayoutManager?.OnViewDetachedFromWindow(detachedView);
			}
		}

		public class ButtonClickListener : Java.Lang.Object, AView.IOnClickListener
		{
			public ButtonHandler? Handler { get; set; }

			public void OnClick(AView? v)
			{
				ButtonManager.OnClick(Handler?.VirtualView, v);
			}
		}

		public class ButtonTouchListener : Java.Lang.Object, AView.IOnTouchListener
		{
			public ButtonHandler? Handler { get; set; }

			public bool OnTouch(AView? v, global::Android.Views.MotionEvent? e)
			{
				ButtonManager.OnTouch(Handler?.VirtualView, v, e);
				return true;
			}
		}
	}
}