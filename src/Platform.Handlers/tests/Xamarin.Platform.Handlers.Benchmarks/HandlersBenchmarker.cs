using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Hosting;
using Xamarin.Forms;
using Xamarin.Platform.Core;
using Xamarin.Platform.Hosting;

namespace Xamarin.Platform.Handlers.Benchmarks
{
	[MemoryDiagnoser]
	public class HandlersBenchmarker
	{
		int _numberOfItems = 100000;
		MockApp _app;

		[GlobalSetup(Target = nameof(GetHandlerUsingDI))]
		public void GlobalSetupForDI()
		{
			_app = App.CreateDefaultBuilder()
						.Init<MockApp>();
		}

		[GlobalSetup(Target = nameof(GetHandlerUsingRegistrar))]
		public void GlobalSetupForRegistrar()
		{
			Registrar.Handlers.Register<IButton, ButtonHandler>();
		}

		[Benchmark]
		public void GetHandlerUsingDI()
		{
			for (int i = 0; i < _numberOfItems; i++)
			{
				var defaultHandler = App.Current.Services.GetHandler(typeof(IButton));
			}
		}

		[Benchmark]
		public void GetHandlerUsingRegistrar()
		{
		
			for (int i = 0; i < _numberOfItems; i++)
			{
				var defaultHandler = Registrar.Handlers.GetHandler<IButton>();
			}
		}
	}

	class MockApp : App
	{
		public MockApp(IHost host) : base(host)
		{
		}
		public override IView CreateView()
		{
			return new Button();
		}
	}
}
