using Xamarin.Forms;
using Xamarin.Platform;
using Xamarin.Platform.Core;

namespace Sample
{
	public class MyApp : IApp
	{
		public MyApp()
		{
			Platform.Init();
		}

		public IView CreateView()
		{
			return new Button() { Text = "Hello I'm a button", BorderColor = Color.Yellow, BorderWidth = 2, CornerRadius = 12, FontSize = 36 };
		}
	}
}