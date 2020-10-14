using System.Linq;
using AppKit;
using Foundation;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public static class PickerExtensions
	{
		public static void UpdateTitle(this NSPopUpButton nSPopUpButton, IPicker picker)
		{
			UpdatePicker(nSPopUpButton, picker);
		}

		public static void UpdateTitleColor(this NSPopUpButton nSPopUpButton, IPicker picker)
		{
			// TODO:
		}

		public static void UpdateTextColor(this NSPopUpButton nSPopUpButton, IPicker picker)
		{
			SetTextColor(nSPopUpButton, picker);
		}

		public static void UpdateSelectedIndex(this NSPopUpButton nSPopUpButton, IPicker picker)
		{
			UpdatePicker(nSPopUpButton, picker);
		}

		public static void UpdateFont(this NSPopUpButton nSPopUpButton, IPicker picker)
		{
			if (nSPopUpButton == null || nSPopUpButton.Menu == null)
				return;

			nSPopUpButton.Menu.Font = picker.ToNSFont();
		}

		public static void UpdateCharacterSpacing(this NSPopUpButton nSPopUpButton, IPicker picker)
		{
			// TODO:
		}

		public static void UpdateHorizontalTextAlignment(this NSPopUpButton nSPopUpButton, IPicker picker)
		{
			// TODO:
		}

		public static void UpdateVerticalTextAlignment(this NSPopUpButton nSPopUpButton, IPicker picker)
		{
			// TODO:
		}

		internal static void UpdatePicker(this NSPopUpButton nSPopUpButton, IPicker picker)
		{
			var selectedIndex = picker.SelectedIndex;
			var items = picker.Items;

			UpdateItems(nSPopUpButton, picker);

			if (items == null || items.Count == 0 || selectedIndex < 0)
				return;

			nSPopUpButton.SelectItem(selectedIndex);
		}

		internal static void SetTextColor(this NSPopUpButton nSPopUpButton, IPicker picker)
		{
			foreach (NSMenuItem it in nSPopUpButton.Items())
			{
				it.AttributedTitle = new NSAttributedString();
			}

			var color = picker.Color;
			if (color != Color.Default && nSPopUpButton.SelectedItem != null)
			{
				NSAttributedString textWithColor = new NSAttributedString(nSPopUpButton.SelectedItem!.Title, foregroundColor: color.ToNative(), paragraphStyle: new NSMutableParagraphStyle() { Alignment = NSTextAlignment.Left });
				nSPopUpButton.SelectedItem!.AttributedTitle = textWithColor;
			}
		}

		internal static void UpdateItems(this NSPopUpButton nSPopUpButton, IPicker picker)
		{
			nSPopUpButton.RemoveAllItems();
			nSPopUpButton.AddItems(picker.Items.ToArray());
		}
	}
}
