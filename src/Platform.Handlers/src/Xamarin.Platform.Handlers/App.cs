using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xamarin.Platform.Hosting;

namespace Xamarin.Platform.Core
{
	public abstract class App : IApp
	{
		IServiceProvider? _serviceProvider;
		IHandlerServiceProvider? _handlerServiceProvider;
		protected App()
		{
			Current = this;
		}

		public static App? Current { get; private set; }

		public IServiceProvider? Services => _serviceProvider;

		public IHandlerServiceProvider? Handlers => _handlerServiceProvider;

		public static AppBuilder CreateDefaultBuilder()
		{
			var builder = new AppBuilder().CreateAppDefaults();
			return builder;
		}

		public void SetServiceProvider(IServiceProvider provider)
		{
			_serviceProvider = provider;
			_handlerServiceProvider = provider.GetRequiredService<IHandlerServiceProvider>();
		}
	}
}
