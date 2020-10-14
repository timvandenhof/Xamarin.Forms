using System;
using System.Collections.Specialized;
using System.Linq;
using Android.App;
using Android.Text;
using Android.Text.Style;
using Xamarin.Forms;
using AResource = Android.Resource;

namespace Xamarin.Platform.Handlers
{
	public partial class PickerHandler : AbstractViewHandler<IPicker, NativePicker>
	{
		NativePicker? _nativePicker;
		   AlertDialog? _dialog;

		protected override NativePicker CreateView()
		{
			_nativePicker = new NativePicker(Context);

			_nativePicker.Click += OnClick;

			if (VirtualView != null)
				((INotifyCollectionChanged)VirtualView.Items).CollectionChanged += OnCollectionChanged;

			return _nativePicker;
		}

		public override void TearDown()
		{
			if (_nativePicker != null)
				_nativePicker.Click -= OnClick;

			if (VirtualView != null)
				((INotifyCollectionChanged)VirtualView.Items).CollectionChanged -= OnCollectionChanged;

			base.TearDown();
		}

		void OnClick(object sender, EventArgs e)
		{
			if (VirtualView == null)
				return;

			if (_dialog == null)
			{
				using (var builder = new AlertDialog.Builder(Context))
				{
					if (VirtualView.TitleColor == Color.Default)
					{
						builder.SetTitle(VirtualView.Title ?? string.Empty);
					}
					else
					{
						var title = new SpannableString(VirtualView.Title ?? string.Empty);
						title.SetSpan(new ForegroundColorSpan(VirtualView.TitleColor.ToNative()), 0, title.Length(), SpanTypes.ExclusiveExclusive);
						builder.SetTitle(title);
					}

					string[] items = VirtualView.Items.ToArray();
					builder.SetItems(items, (s, e) =>
					{
						var selectedIndex = e.Which;
						VirtualView.SelectedIndex = selectedIndex;
						VirtualView.SelectedIndexChanged();
						TypedNativeView?.UpdatePicker(VirtualView);
					});

					builder.SetNegativeButton(AResource.String.Cancel, (o, args) => { });

					_dialog = builder.Create();
				}

				if (_dialog == null)
					return;

				_dialog.SetCanceledOnTouchOutside(true);

				_dialog.DismissEvent += (sender, args) =>
				{
					_dialog.Dispose();
					_dialog = null;
				};

				_dialog.Show();
			}
		}

		void OnCollectionChanged(object sender, EventArgs e)
		{
			if (VirtualView == null || TypedNativeView == null)
				return;

			TypedNativeView.UpdatePicker(VirtualView);
		}
	}
}