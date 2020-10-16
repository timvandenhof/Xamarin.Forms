using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
	public class Button : FontElement, IButton
	{
		public const int DefaultCornerRadius = -1;
		public const int DefaultBorderWidth = -1;

		public string Text { get; set; }

		public Color Color { get; set; }

		public int CornerRadius { get; set; } = DefaultCornerRadius;

		public Color BorderColor { get; set; }

		public double BorderWidth { get; set; } = DefaultBorderWidth;

		public TextTransform TextTransform { get; set; }

		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }

		public double CharacterSpacing { get; set; }

		public LineBreakMode LineBreakMode { get; set; }

		public ButtonContentLayout ContentLayout { get; set; }

		public Thickness Padding { get; set; }

		public ICommand Command { get; set; }

		public object CommandParameter { get; set; }

		string IText.UpdateTransformedText(string source, TextTransform textTransform)
			=> TextTransformUtilites.GetTransformedText(source, textTransform);

		public Action Pressed { get; set; }
		public Action Released { get; set; }
		public Action Clicked { get; set; }

		void IButton.Pressed() => Pressed?.Invoke();
		void IButton.Released() => Released?.Invoke();

		void IButton.Clicked()
		{
			Command?.Execute(CommandParameter);
			Clicked?.Invoke();
		}
	}
}