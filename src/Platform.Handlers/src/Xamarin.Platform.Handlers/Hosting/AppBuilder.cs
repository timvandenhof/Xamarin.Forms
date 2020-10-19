using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xamarin.Platform.Core;
using Xamarin.Platform.Handlers;

namespace Xamarin.Platform.Hosting
{
	public class AppBuilder : HostBuilder, IDisposable
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
			_cts = new CancellationTokenSource();
		}

		//CreateAppDefaults will set the common things like:
		// - Set the ContentRoot
		// - Register the basic handlers
		public AppBuilder CreateAppDefaults()
		{
		 	this.UseContentRoot(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
			UseXamarinHandlers();
			return this;
		}

		public new AppBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
		{
			_configureServicesActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
			return this;
		}

		public TApplication Init<TApplication>() where TApplication : class, IApp
		{
			BuildAndRegisterHandlersProvider();

			var app = Activator.CreateInstance(typeof(TApplication));
			base.ConfigureServices((context, services) =>
			{
				foreach (Action<HostBuilderContext, IServiceCollection> configureServicesAction in _configureServicesActions)
				{
					configureServicesAction(context, services);
				}

				services.AddSingleton<IHostLifetime, AppLifetime>();

				AppLoader.ConfigureAppServices<TApplication>(context, services, app);
			});

			RegisterAssemblies<TApplication>();

			_host = Build();

			(app as App)?.SetServiceProvider(_host.Services);

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
	
		IHostBuilder BuildAndRegisterHandlersProvider()
		{
			var handlersProvider = _handlersCollection.BuildHandlerServiceProvider();
			ConfigureServices((context, collection) => collection.AddSingleton<IHandlerServiceProvider>(handlersProvider));

			return this;
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
