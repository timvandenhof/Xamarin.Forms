﻿using System;
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
		IHostBuilder _builder;

		[GlobalSetup(Target = nameof(GetHandlerUsingDI))]
		public void GlobalSetupForDI()
		{
			var builder = App.CreateDefaultBuilder();
			_app = builder.Init<MockApp>();
		}

		[GlobalSetup(Target = nameof(GetHandlerUsingRegistrar))]
		public void GlobalSetupForRegistrar()
		{
			Registrar.Handlers.Register<IButton, ButtonHandler>();
		}


		[IterationSetup(Target = nameof(RegisterHandlerUsingDI))]
		public void GlobalSetupForDiWithHandlersRegistration()
		{
			_builder = new HostBuilder();
		}

		[Benchmark]
		public void RegisterHandlerUsingDI()
		{
			for (int i = 0; i < _numberOfItems; i++)
			{
				_builder.RegisterHandler<IButton, ButtonHandler>();
			}
		}

		[Benchmark]
		public void RegisterHandlerUsingRegistrar()
		{
			for (int i = 0; i < _numberOfItems; i++)
			{
				Registrar.Handlers.Register<IButton, ButtonHandler>();
			}
		}

		[Benchmark]
		public void GetHandlerUsingDI()
		{
			for (int i = 0; i < _numberOfItems; i++)
			{
				var defaultHandler = _app.Handlers.GetHandler(typeof(IButton));
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
