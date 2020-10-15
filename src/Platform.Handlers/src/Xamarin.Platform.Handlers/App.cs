using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xamarin.Platform.Hosting;

namespace Xamarin.Platform.Core
{
	public abstract class App : IApp
	{
		protected App(IHost host)
		{
			Host = host;
			Handlers = Services.GetRequiredService<IHandlerServiceProvider>();
			Current = this;
		}

		public static App? Current { get; private set; }

		public IHost Host { get; private set; }

		public IServiceProvider Services => Host.Services;

		public IHandlerServiceProvider Handlers { get; private set; }

		public abstract IView CreateView();

		public static AppBuilder CreateDefaultBuilder()
		{
			var builder = new AppBuilder().CreateAppDefaults();
			return builder;
		}
	}
}
