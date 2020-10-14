using System;
using System.Collections.Specialized;
using UIKit;
using RectangleF = CoreGraphics.CGRect;

namespace Xamarin.Platform.Handlers
{
	public partial class PickerHandler : AbstractViewHandler<IPicker, NativePicker>
	{
		UIPickerView? _pickerView;

		protected override NativePicker CreateView()
		{
			_pickerView = new UIPickerView();

			var nativePicker = new NativePicker(_pickerView) { BorderStyle = UITextBorderStyle.RoundedRect };

			nativePicker.EditingChanged += OnEditing;

			var width = UIScreen.MainScreen.Bounds.Width;
			var toolbar = new UIToolbar(new RectangleF(0, 0, width, 44)) { BarStyle = UIBarStyle.Default, Translucent = true };
			var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

			var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (o, a) =>
			{
				var pickerSource = (PickerSource)_pickerView.Model;

				if (VirtualView?.SelectedIndex == -1 && VirtualView.Items != null && VirtualView.Items.Count > 0)
				{
					TypedNativeView?.SetSelectedIndex(VirtualView, 0);
				}

				nativePicker.Text = pickerSource.SelectedItem;
				nativePicker.ResignFirstResponder();
			});

			toolbar.SetItems(new[] { spacer, doneButton }, false);

			nativePicker.InputView = _pickerView;
			nativePicker.InputAccessoryView = toolbar;

			nativePicker.InputView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
			nativePicker.InputAccessoryView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;

			if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
			{
				nativePicker.InputAssistantItem.LeadingBarButtonGroups = null;
				nativePicker.InputAssistantItem.TrailingBarButtonGroups = null;
			}

			nativePicker.AccessibilityTraits = UIAccessibilityTrait.Button;

			_pickerView.Model = new PickerSource(VirtualView);

			if (VirtualView != null)
				((INotifyCollectionChanged)VirtualView.Items).CollectionChanged += OnCollectionChanged;

			return nativePicker;
		}

		public override void TearDown()
		{
			if (_pickerView != null)
			{
				if (_pickerView.Model != null)
				{
					_pickerView.Model.Dispose();
					_pickerView.Model = null;
				}

				_pickerView.RemoveFromSuperview();
				_pickerView.Dispose();
				_pickerView = null;
			}

			if (TypedNativeView != null)
			{
				TypedNativeView.EditingChanged -= OnEditing;
			}

			if (VirtualView != null)
				((INotifyCollectionChanged)VirtualView.Items).CollectionChanged -= OnCollectionChanged;

			base.TearDown();
		}

		void OnCollectionChanged(object sender, EventArgs e)
		{
			if (VirtualView == null || TypedNativeView == null)
				return;

			TypedNativeView.UpdatePicker(VirtualView);
		}

		void OnEditing(object sender, EventArgs eventArgs)
		{
			if (VirtualView == null || TypedNativeView == null)
				return;

			// Reset the TextField's Text so it appears as if typing with a keyboard does not work.
			var selectedIndex = VirtualView.SelectedIndex;
			var items = VirtualView.Items;
			TypedNativeView.Text = selectedIndex == -1 || items == null ? "" : items[selectedIndex];

			// Also clears the undo stack (undo/redo possible on iPads)
			TypedNativeView.UndoManager.RemoveAllActions();
		}
	}
		
	class PickerSource : UIPickerViewModel
	{
		IPicker? _virtualView;
		bool _disposed;

		public PickerSource(IPicker? virtualView)
		{
			_virtualView = virtualView;
		}

		public int SelectedIndex { get; internal set; }

		public string? SelectedItem { get; internal set; }

		public override nint GetComponentCount(UIPickerView picker)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
		{
			return _virtualView?.Items != null ? _virtualView.Items.Count : 0;
		}

		public override string GetTitle(UIPickerView picker, nint row, nint component)
		{
			return _virtualView != null ? _virtualView.Items[(int)row] : string.Empty;
		}

		public override void Selected(UIPickerView picker, nint row, nint component)
		{
			if (_virtualView?.Items.Count == 0)
			{
				SelectedItem = null;
				SelectedIndex = -1;
			}
			else
			{
				SelectedItem = _virtualView?.Items[(int)row];
				SelectedIndex = (int)row;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			_disposed = true;

			if (disposing)
				_virtualView = null;

			base.Dispose(disposing);
		}
	}
}