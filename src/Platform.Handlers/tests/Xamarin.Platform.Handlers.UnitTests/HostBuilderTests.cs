using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using NUnit.Framework;
using Xamarin.Forms;
using Xamarin.Platform.Core;
using Xamarin.Platform.Hosting;

namespace Xamarin.Platform.Handlers.Tests
{
	[TestFixture]
	public class HostBuilderTests
	{
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

		interface IMockView : IView
		{

		}

		class MockView : IMockView
		{
			public bool IsEnabled => throw new NotImplementedException();

			public Color BackgroundColor => throw new NotImplementedException();

			public Rectangle Frame => throw new NotImplementedException();

			public IViewHandler Handler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

			public IFrameworkElement Parent => throw new NotImplementedException();

			public SizeRequest DesiredSize => throw new NotImplementedException();

			public bool IsMeasureValid => throw new NotImplementedException();

			public bool IsArrangeValid => throw new NotImplementedException();

			public void Arrange(Rectangle bounds)
			{
				throw new NotImplementedException();
			}

			public void InvalidateArrange()
			{
				throw new NotImplementedException();
			}

			public void InvalidateMeasure()
			{
				throw new NotImplementedException();
			}

			public SizeRequest Measure(double widthConstraint, double heightConstraint)
			{
				throw new NotImplementedException();
			}
		}

		class NativeMockView
		{

		}

		class MockViewHadler : AbstractViewHandler<IMockView, NativeMockView>
		{
			public static PropertyMapper<IMockView, MockViewHadler> MockViewMapper = new PropertyMapper<IMockView, MockViewHadler>(ViewHandler.ViewMapper)
			{

			};

			public MockViewHadler() : base(MockViewMapper)
			{

			}

			public MockViewHadler(PropertyMapper mapper) : base(mapper ?? MockViewMapper)
			{

			}
			protected override NativeMockView CreateView() => new NativeMockView();
		}

		class MockButtonHadler : ButtonHandler
		{
		}

		class MockButton : View, IButton
		{
			public string Text => throw new NotImplementedException();

			public Color Color => throw new NotImplementedException();

			public Font Font => throw new NotImplementedException();

			public TextTransform TextTransform => throw new NotImplementedException();

			public double CharacterSpacing => throw new NotImplementedException();

			public FontAttributes FontAttributes => throw new NotImplementedException();

			public string FontFamily => throw new NotImplementedException();

			public double FontSize => throw new NotImplementedException();

			public TextAlignment HorizontalTextAlignment => throw new NotImplementedException();

			public TextAlignment VerticalTextAlignment => throw new NotImplementedException();

			public void Clicked()
			{
				throw new NotImplementedException();
			}

			public void Pressed()
			{
				throw new NotImplementedException();
			}

			public void Released()
			{
				throw new NotImplementedException();
			}
		}

		[Test]
		public void CanBuildAHost()
		{
			var host = App.CreateDefaultBuilder()
							  .Build();
			Assert.IsNotNull(host);
		}

		[Test]
		public void CanGetApp()
		{
			var app = App.CreateDefaultBuilder()
							  .Init<MockApp>();
			Assert.IsNotNull(app);
			Assert.IsInstanceOf(typeof(MockApp), app);
		}

		[Test]
		public void CanGetStaticApp()
		{
			var app = App.CreateDefaultBuilder()
						  .Init<MockApp>();

			Assert.IsNotNull(App.Current);
			Assert.AreEqual(App.Current, app);
		}

		[Test]
		public void CanGetServices()
		{
			var app = App.CreateDefaultBuilder()
							  .Init<MockApp>();

			Assert.IsNotNull(app.Services);
		}

		[Test]
		public void CanGetStaticServices()
		{
			var app = App.CreateDefaultBuilder()
							  .Init<MockApp>();

			Assert.IsNotNull(App.Current.Services);
			Assert.AreEqual(app.Services, App.Current.Services);
		}

		[Test]
		public void CanRegisterAndGetHandler()
		{
			var app = App.CreateDefaultBuilder()
							.RegisterHandler<IMockView, MockViewHadler>()
							.Init<MockApp>();

			var handler = App.Current.Handlers.GetHandler(typeof(IMockView));
			Assert.IsNotNull(handler);
			Assert.IsInstanceOf(typeof(MockViewHadler), handler);
		}

		[Test]
		public void CanRegisterAndGetHandlerWithDictionary()
		{
			var app = App.CreateDefaultBuilder()
							.RegisterHandlers(new Dictionary<Type, Type> { { typeof(IMockView), typeof(MockViewHadler) } })
							.Init<MockApp>();

			var handler = App.Current.Handlers.GetHandler(typeof(IMockView));
			Assert.IsNotNull(handler);
			Assert.IsInstanceOf(typeof(MockViewHadler), handler);
		}

		[Test]
		public void CanRegisterAndGetHandlerForType()
		{
			var app = App.CreateDefaultBuilder()
							.RegisterHandler<IMockView, MockViewHadler>()
							.Init<MockApp>();

			var handler = App.Current.Handlers.GetHandler(typeof(MockView));
			Assert.IsNotNull(handler);
			Assert.IsInstanceOf(typeof(MockViewHadler), handler);
		}

		[Test]
		public void DefaultHandlersAreRegistered()
		{
			var app = App.CreateDefaultBuilder()
							.Init<MockApp>();

			var handler = App.Current.Handlers.GetHandler(typeof(IButton));
			Assert.IsNotNull(handler);
			Assert.IsInstanceOf(typeof(ButtonHandler), handler);
		}

		[Test]
		public void CanSpecifyHandler()
		{
			var app = App.CreateDefaultBuilder()
							.RegisterHandler<MockButton, MockButtonHadler>()
							.Init<MockApp>();

			var defaultHandler = App.Current.Handlers.GetHandler(typeof(IButton));
			var specificHandler = App.Current.Handlers.GetHandler(typeof(MockButton));
			Assert.IsNotNull(defaultHandler);
			Assert.IsNotNull(specificHandler);
			Assert.IsInstanceOf(typeof(ButtonHandler), defaultHandler);
			Assert.IsInstanceOf(typeof(MockButtonHadler), specificHandler);
		}

		[Test]
		public void Get100Handlers()
		{
			int iterations = 10000;
			var app = App.CreateDefaultBuilder()
						 .Init<MockApp>();

			Stopwatch watch = Stopwatch.StartNew();
			for (int i = 0; i < iterations; i++)
			{
				var defaultHandler = App.Current.Handlers.GetHandler(typeof(Button));
				Assert.NotNull(defaultHandler);
			}
			watch.Stop();
			var total = watch.ElapsedMilliseconds;
			watch.Reset();
			Registrar.Handlers.Register<IButton, ButtonHandler>();
			watch.Start();
			for (int i = 0; i < iterations; i++)
			{
				var defaultHandler = Registrar.Handlers.GetHandler<Button>();
				Assert.NotNull(defaultHandler);
			}
			watch.Stop();
			var totalRegistrar = watch.ElapsedMilliseconds;

			Assert.LessOrEqual(total, totalRegistrar);
			Console.WriteLine($"Elapsed time DI: {total} and Registrar: {totalRegistrar}");
		}

		IHostBuilder _builder;

		[Test]
		public void Register100Handlers()
		{
			int iterations = 10000;
			_builder = new HostBuilder()
				.UseContentRoot(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
			for (int i = 0; i < iterations; i++)
			{
				_builder.RegisterHandler<IButton, ButtonHandler>();
			}
			var host = _builder.Build();
		}
	}
}
