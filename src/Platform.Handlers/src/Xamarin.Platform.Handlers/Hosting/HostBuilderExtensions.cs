using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xamarin.Platform.Core;
using Xamarin.Platform.Handlers;

namespace Xamarin.Platform.Hosting
{
	public static class HostBuilderExtensions
	{
		static HandlerServiceCollection s_handlersCollection = new HandlerServiceCollection();
		public static IHostBuilder RegisterHandlers(this IHostBuilder hostBuilder, Dictionary<Type, Type> handlers)
		{
			foreach (var handler in handlers)
				s_handlersCollection.AddTransient(handler.Key, handler.Value);

			return BuildAndRegisterHandlersProvider(hostBuilder);
		}

		public static IHostBuilder RegisterHandler<TType, TTypeRender>(this IHostBuilder hostBuilder)
			where TType : IFrameworkElement
			where TTypeRender : IViewHandler
		{
			s_handlersCollection.AddTransient(typeof(TType), typeof(TTypeRender));

			return BuildAndRegisterHandlersProvider(hostBuilder);
		}

		public static IHostBuilder UseXamarinHandlers(this IHostBuilder hostBuilder)
		{
			hostBuilder.RegisterHandlers(new Dictionary<Type, Type>
			{
				{  typeof(IButton), typeof(ButtonHandler) }
			});
			return hostBuilder;
		}

		public static IHostBuilder UseXamarinImageHandlers(this IHostBuilder hostBuilder)
		{
			hostBuilder.ConfigureServices((context, collection) =>
			{
				//register ImageHandlers
				//collection.AddSingleton<IUriImageHandler,UriImageHandler>()
			});
			return hostBuilder;
		}

		public static TApplication Init<TApplication>(this IHostBuilder hostBuilder) where TApplication : class, IApp
		{
			hostBuilder.ConfigureServices((context, collection) => collection.AddSingleton<TApplication>());
			var host = hostBuilder.Build();
			var app = host.Services.GetRequiredService<TApplication>();
			return app;
		}

		static IHostBuilder BuildAndRegisterHandlersProvider(IHostBuilder hostBuilder)
		{
			var handlersProvider = s_handlersCollection.BuildHandlerServiceProvider();
			hostBuilder.ConfigureServices((context, collection) => collection.AddSingleton(handlersProvider));

			return hostBuilder;
		}
	}
}
