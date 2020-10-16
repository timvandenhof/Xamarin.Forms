using Xamarin.Forms;

namespace Xamarin.Platform
{
	public interface IFont : IView
	{
		Font Font { get; }
		FontAttributes FontAttributes { get; }
		string FontFamily { get; }
		double FontSize { get; }
	}
}