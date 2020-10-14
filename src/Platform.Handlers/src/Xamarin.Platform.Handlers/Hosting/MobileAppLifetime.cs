using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Xamarin.Platform.Hosting
{
	class MobileAppLifetime : IHostLifetime, IDisposable
	{
		readonly ManualResetEvent _shutdownBlock = new ManualResetEvent(false);
		CancellationTokenRegistration _applicationStartedRegistration;
		CancellationTokenRegistration _applicationStoppingRegistration;

		public MobileAppLifetime(IOptions<MobileAppLifetimeOptions> options, IHostEnvironment environment, IHostApplicationLifetime applicationLifetime, IOptions<HostOptions> hostOptions)
			: this(options, environment, applicationLifetime, hostOptions, NullLoggerFactory.Instance) { }

		public MobileAppLifetime(IOptions<MobileAppLifetimeOptions> options, IHostEnvironment environment, IHostApplicationLifetime applicationLifetime, IOptions<HostOptions> hostOptions, ILoggerFactory loggerFactory)
		{
			Options = options?.Value ?? throw new ArgumentNullException(nameof(options));
			Environment = environment ?? throw new ArgumentNullException(nameof(environment));
			ApplicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
			HostOptions = hostOptions?.Value ?? throw new ArgumentNullException(nameof(hostOptions));
			Logger = loggerFactory.CreateLogger("Microsoft.Hosting.Lifetime");
		}

		MobileAppLifetimeOptions Options { get; }

		IHostEnvironment Environment { get; }

		IHostApplicationLifetime ApplicationLifetime { get; }

		HostOptions HostOptions { get; }

		ILogger Logger { get; }

		public Task WaitForStartAsync(CancellationToken cancellationToken)
		{
			if (!Options.SuppressStatusMessages)
			{
				_applicationStartedRegistration = ApplicationLifetime.ApplicationStarted.Register(state =>
				{
					((MobileAppLifetime)state).OnApplicationStarted();
				},
				this);
				_applicationStoppingRegistration = ApplicationLifetime.ApplicationStopping.Register(state =>
				{
					((MobileAppLifetime)state).OnApplicationStopping();
				},
				this);
			}

			AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

			return Task.CompletedTask;
		}

		void OnApplicationStarted()
		{
			Logger.LogInformation("Application started.");
			Logger.LogInformation("Hosting environment: {envName}", Environment.EnvironmentName);
			Logger.LogInformation("Content root path: {contentRoot}", Environment.ContentRootPath);
		}

		void OnApplicationStopping()
		{
			Logger.LogInformation("Application is shutting down...");
		}

		void OnProcessExit(object sender, EventArgs e)
		{
			ApplicationLifetime.StopApplication();
			if (!_shutdownBlock.WaitOne(HostOptions.ShutdownTimeout))
			{
				Logger.LogInformation("Waiting for the host to be disposed. Ensure all 'IHost' instances are wrapped in 'using' blocks.");
			}
			_shutdownBlock.WaitOne();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			// There's nothing to do here
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_shutdownBlock.Set();

			AppDomain.CurrentDomain.ProcessExit -= OnProcessExit;

			_applicationStartedRegistration.Dispose();
			_applicationStoppingRegistration.Dispose();
		}
	}
}
