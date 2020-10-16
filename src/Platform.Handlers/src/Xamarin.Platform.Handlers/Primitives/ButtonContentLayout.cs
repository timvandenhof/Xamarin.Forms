using System.Diagnostics;

namespace Xamarin.Forms
{
	[DebuggerDisplay("Image Position = {Position}, Spacing = {Spacing}")]
	public sealed class ButtonContentLayout
	{
		public enum ImagePosition
		{
			Left,
			Top,
			Right,
			Bottom
		}

		public ButtonContentLayout(ImagePosition position, double spacing)
		{
			Position = position;
			Spacing = spacing;
		}

		public ImagePosition Position { get; }

		public double Spacing { get; }

		public override string ToString()
		{
			return $"Image Position = {Position}, Spacing = {Spacing}";
		}
	}
}