using System;
using System.Collections.Generic;
using System.Text;
using Sample.Services;
using Sample.ViewModel;
using Xamarin.Forms;

namespace Sample.Pages
{
	public class MainPage : ContentPage
	{
		public MainPage(MainPageViewModel viewModel)
		{
			BindingContext = viewModel;

			var btn = new Xamarin.Forms.Button();
			btn.SetBinding(Xamarin.Forms.Button.TextProperty, "Text");

			Content = btn;
		}
	}
}
