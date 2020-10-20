using System;
using System.Collections.Generic;
using System.Text;
using Sample.Services;
using Sample.ViewModel;
using Xamarin.Forms;
using Xamarin.Platform;
using Xamarin.Platform.Hosting;

namespace Sample.Pages
{
	public class MainPage : ContentPage, IStartup
	{
		public MainPage(MainPageViewModel viewModel)
		{
			BindingContext = viewModel;
		}

		public IView GetContentView()
		{
			var verticalStack = new Xamarin.Platform.VerticalStackLayout() { Spacing = 5, BackgroundColor = Color.AntiqueWhite };
			var horizontalStack = new HorizontalStackLayout() { Spacing = 2 };

			var label = new Label { Text = "This top part is a Xamarin.Platform.VerticalStackLayout" };

			verticalStack.Add(label);

			var button = new Xamarin.Forms.Button();
			button.BindingContext = BindingContext;
			button.SetBinding(Xamarin.Forms.Button.TextProperty, "Text");
			var button2 = new Button()
			{
				Color = Color.Green,
				Text = "Hello I'm a button",
				BackgroundColor = Color.Purple
			};

			horizontalStack.Add(button);
			horizontalStack.Add(button2);
			horizontalStack.Add(new Label { Text = "And these buttons are in a HorizontalStackLayout" });

			verticalStack.Add(horizontalStack);

			return verticalStack;
		}
	}
}
