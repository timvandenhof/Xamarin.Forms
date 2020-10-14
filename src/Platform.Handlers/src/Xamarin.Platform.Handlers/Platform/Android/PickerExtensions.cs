using Android.Util;
using Xamarin.Forms;
using AColor = Android.Graphics.Color;

namespace Xamarin.Platform
{
	public static class PickerExtensions
	{
		static TextColorSwitcher? _textColorSwitcher;

		public static void UpdateTitle(this NativePicker nativePicker, IPicker picker) =>
			UpdatePicker(nativePicker, picker);

		public static void UpdateTitleColor(this NativePicker nativePicker, IPicker picker) =>
			UpdatePicker(nativePicker, picker);

		public static void UpdateTextColor(this NativePicker nativePicker, IPicker picker)
		{
			if (_textColorSwitcher == null)
				_textColorSwitcher = new TextColorSwitcher(nativePicker.TextColors);

			_textColorSwitcher?.UpdateTextColor(nativePicker, picker.Color);
		}

		public static void UpdateSelectedIndex(this NativePicker nativePicker, IPicker picker) =>
			UpdatePicker(nativePicker, picker);

		public static void UpdateFont(this NativePicker nativePicker, IPicker picker)
		{
			nativePicker.Typeface = picker.ToTypeface();
			nativePicker.SetTextSize(ComplexUnitType.Sp, (float)picker.FontSize);
		}

		public static void UpdateCharacterSpacing(this NativePicker nativePicker, IPicker picker)
		{
			if (NativeVersion.IsAtLeast(21))
			{
				nativePicker.LetterSpacing = picker.CharacterSpacing.ToEm();
			}
		}

		public static void UpdateHorizontalTextAlignment(this NativePicker nativePicker, IPicker picker)
		{
			nativePicker.SetTextAlignment(picker);
		}

		public static void UpdateVerticalTextAlignment(this NativePicker nativePicker, IPicker picker)
		{
			nativePicker.SetTextAlignment(picker);
		}

		internal static void UpdatePicker(this NativePicker nativePicker, IPicker picker) =>
			nativePicker.UpdatePicker(picker, nativePicker.CurrentHintTextColor);

		internal static void UpdatePicker(this NativePicker nativePicker, IPicker picker, int currentHintTextColor)
		{
			nativePicker.Hint = picker.Title;

			if (picker.TitleColor != Color.Default)
				nativePicker.SetHintTextColor(picker.TitleColor.ToNative());
			else
				nativePicker.SetHintTextColor(new AColor(currentHintTextColor));

			if (picker.SelectedIndex == -1 || picker.Items == null || picker.SelectedIndex >= picker.Items.Count)
				nativePicker.Text = null;
			else
				nativePicker.Text = picker.Items[picker.SelectedIndex];

			nativePicker.UpdateSelectedItem(picker);
		}

		internal static void UpdateSelectedItem(this NativePicker nativePicker, IPicker picker)
		{
			if (picker == null || nativePicker == null)
				return;

			int index = picker.SelectedIndex;

			if (index == -1)
			{
				picker.SelectedItem = null;
				return;
			}

			if (picker.ItemsSource != null)
			{
				picker.SelectedItem = picker.ItemsSource[index];
				return;
			}

			picker.SelectedItem = picker.Items[index];
		}

		internal static void SetTextAlignment(this NativePicker nativePicker, IPicker picker)
		{
			nativePicker.Gravity = picker.HorizontalTextAlignment.ToHorizontalGravityFlags() | picker.VerticalTextAlignment.ToVerticalGravityFlags();
		}
	}
}