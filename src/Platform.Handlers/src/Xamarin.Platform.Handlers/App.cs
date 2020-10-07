using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xamarin.Platform.Hosting;

namespace Xamarin.Platform.Core
{
	public abstract class App : IApp
	{
		static App? s_current;
		readonly IHost _host;

		protected App(IHost host)
		{
			_host = host;
			s_current = this;
		}

		public static App? Current => s_current;

		public IHost Host => _host;
		public IServiceProvider Services => Host.Services;

		public abstract IView CreateView();

		public static IHostBuilder CreateDefaultBuilder<TApplication>() where TApplication : class, IApp
		{
			var systemDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var builder = new HostBuilder()
			.UseContentRoot(systemDir)
			.UseDefaultServiceProvider((context, options) =>
			{
				options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
			})
			.ConfigureServices((context, collection) => collection.AddSingleton<TApplication>())
			.UseXamarinHandlers();

			return builder;
		}
	}
}
