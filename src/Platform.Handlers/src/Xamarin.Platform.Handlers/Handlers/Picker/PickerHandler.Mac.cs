using System;
using System.Collections.Specialized;
using AppKit;

namespace Xamarin.Platform.Handlers
{
	public partial class PickerHandler : AbstractViewHandler<IPicker, NSPopUpButton>
	{
		NSPopUpButton? _nSPopUpButton;

		protected override NSPopUpButton CreateView()
		{
			_nSPopUpButton = new NSPopUpButton();

			_nSPopUpButton.Activated += OnComboBoxSelectionChanged;

			if (VirtualView != null)
				((INotifyCollectionChanged)VirtualView.Items).CollectionChanged += OnCollectionChanged;

			return _nSPopUpButton;
		}

		public override void TearDown()
		{
			if (_nSPopUpButton != null)
				_nSPopUpButton.Activated -= OnComboBoxSelectionChanged;

			if (VirtualView != null)
				((INotifyCollectionChanged)VirtualView.Items).CollectionChanged -= OnCollectionChanged;

			base.TearDown();
		}

		void OnComboBoxSelectionChanged(object sender, EventArgs e)
		{
			if (VirtualView == null || TypedNativeView == null)
				return;

			VirtualView.SelectedIndex = (int)TypedNativeView.IndexOfSelectedItem;
		}

		void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (VirtualView == null || TypedNativeView == null)
				return;

			TypedNativeView.UpdatePicker(VirtualView);
		}
	}
}