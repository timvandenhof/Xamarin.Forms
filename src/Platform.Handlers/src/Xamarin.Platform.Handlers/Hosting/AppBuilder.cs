using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xamarin.Platform.Core;
using Xamarin.Platform.Handlers;

namespace Xamarin.Platform.Hosting
{
	public class AppBuilder : IDisposable
	{

		readonly HandlerServiceCollection _handlersCollection = new HandlerServiceCollection();
		readonly List<Action<HostBuilderContext, IServiceCollection>> _configureServicesActions = new List<Action<HostBuilderContext, IServiceCollection>>();
		IHost? _host;
		bool _disposed;
		readonly CancellationTokenSource _cts;
		Assembly? _appAssembly;
		Assembly? _callingAssembly;

		public AppBuilder()
		{
			HostBuilder = new HostBuilder();
			_cts = new CancellationTokenSource();
		}

		//We expose the Builder so we allow the user to do HostConnfiguration like Logging
		public IHostBuilder HostBuilder { get; }

		//CreateAppDefaults will set the common things like:
		// - Set the ContentRoot
		// - Register the basic handlers
		public AppBuilder CreateAppDefaults()
		{
			HostBuilder.UseContentRoot(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
			UseXamarinHandlers();
			return this;
		}

		public AppBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
		{
			_configureServicesActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
			return this;
		}

		public TApplication Init<TApplication>() where TApplication : class, IApp
		{
			BuildAndRegisterHandlersProvider(HostBuilder);
			
			HostBuilder.ConfigureServices((context, collection) =>
			{
				foreach (Action<HostBuilderContext, IServiceCollection> configureServicesAction in _configureServicesActions)
				{
					configureServicesAction(context, collection);
				}

				collection.AddSingleton<IHostLifetime, AppLifetime>();
				collection.AddSingleton<IApp, TApplication>();
			});

			RegisterAssemblies<TApplication>();
			
			_host = HostBuilder.Build();
			
			return (TApplication)_host.Services.GetRequiredService<IApp>();
		}

		public async void Start()
		{
			if (_host != null)
				await _host.RunAsync(_cts.Token);

		}

		public async void Stop()
		{
			if (_host != null)
				await _host.StopAsync();
			_cts.Cancel();
		}

		public AppBuilder RegisterHandlers(Dictionary<Type, Type> handlers)
		{

			foreach (var handler in handlers)
				_handlersCollection.AddTransient(handler.Key, handler.Value);

			return this;
		}

		public AppBuilder RegisterHandler<TType, TTypeRender>()
			where TType : IFrameworkElement
			where TTypeRender : IViewHandler
		{
			_handlersCollection.AddTransient(typeof(TType), typeof(TTypeRender));

			return this;
		}

		public AppBuilder UseXamarinHandlers()
		{
			RegisterHandlers(new Dictionary<Type, Type>
			{
				{  typeof(IButton), typeof(ButtonHandler) }
			});
			return this;
		}

		IHostBuilder BuildAndRegisterHandlersProvider(IHostBuilder hostBuilder)
		{
			var handlersProvider = _handlersCollection.BuildHandlerServiceProvider();
			hostBuilder.ConfigureServices((context, collection) => collection.AddSingleton(handlersProvider));

			return hostBuilder;
		}

		void RegisterAssemblies<TApplication>() where TApplication : class, IApp
		{
			_callingAssembly = Assembly.GetCallingAssembly();
			_appAssembly = typeof(TApplication).Assembly;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Stop();
					_host?.Dispose();
				}
				_disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
