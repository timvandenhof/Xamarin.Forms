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

		public static IHostBuilder CreateDefaultBuilder()
		{
			var builder = new HostBuilder()
			.UseContentRoot(Environment.GetFolderPath(Environment.SpecialFolder.Personal))
			.UseXamarinHandlers();

			return builder;
		}
	}
}
