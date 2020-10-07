using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Platform;
using Xamarin.Platform.Handlers;
using RegistrarHandlers = Xamarin.Platform.Registrar;

namespace Sample
{
	public static class Platform
	{
		public static IHostBuilder Init(this IHostBuilder builder)
		{
			if (HasInit)
				return;

			HasInit = true;

			//RegistrarHandlers.Handlers.Register<Layout, LayoutHandler>();

			RegistrarHandlers.Handlers.Register<Button, ButtonHandler>();
			RegistrarHandlers.Handlers.Register<Xamarin.Platform.VerticalStackLayout, LayoutHandler>();
			RegistrarHandlers.Handlers.Register<Xamarin.Platform.HorizontalStackLayout, LayoutHandler>();
			RegistrarHandlers.Handlers.Register<Xamarin.Forms.FlexLayout, LayoutHandler>();
			RegistrarHandlers.Handlers.Register<Xamarin.Forms.StackLayout, LayoutHandler>();
			//RegistrarHandlers.Handlers.Register<Entry, EntryHandler>();
			RegistrarHandlers.Handlers.Register<Label, LabelHandler>();
		}
	}
}