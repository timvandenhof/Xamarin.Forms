using Xamarin.Forms;

namespace Xamarin.Platform
{
	public interface IText : IFont, ITextAlignment
	{
		string Text { get; }

		Color Color { get; }

		TextTransform TextTransform { get; }

		double CharacterSpacing { get; }

		string UpdateTransformedText(string source, TextTransform textTransform);
	}
}