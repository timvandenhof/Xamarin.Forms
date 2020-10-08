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
		internal interface IRenderer<TType> where TType : IFrameworkElement { }

		class Renderer<TType, TTypeRender> : IRenderer<TType>
				where TType : IFrameworkElement
				where TTypeRender : IViewHandler
		{
		}

		public static IHostBuilder RegisterHandlers(this IHostBuilder hostBuilder, Dictionary<Type, Type> handlers)
		{
			hostBuilder.ConfigureServices(s =>
			{
				foreach (var handler in handlers)
				{
					var keyType = handler.Key;
					var valueType = handler.Value;
					var genericType = typeof(IRenderer<>).MakeGenericType(keyType);
					var handlerType = typeof(Renderer<,>).MakeGenericType(keyType, valueType);
					s.AddTransient(genericType, handlerType);
					//s.AddTransient(typeof(IRenderer<IButton>), typeof(Renderer<IButton, ButtonHandler>));
				}
			});
			return hostBuilder;
		}

		public static IHostBuilder RegisterHandler<TType, TTypeRender>(this IHostBuilder hostBuilder)
			where TType : IFrameworkElement
			where TTypeRender : IViewHandler
		{
			hostBuilder.ConfigureServices(s =>
			{
				s.AddTransient(typeof(IRenderer<TType>), typeof(Renderer<TType, TTypeRender>));
			});

			return hostBuilder;
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
	}
}
