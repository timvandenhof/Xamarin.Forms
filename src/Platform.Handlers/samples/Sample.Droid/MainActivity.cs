using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using Xamarin.Platform;
using Xamarin.Platform.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sample.Services;
using Xamarin.Platform.Hosting;
using Xamarin.Platform.Handlers;
using System;
using System.Collections.Generic;

namespace Sample.Droid
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
		ViewGroup _page;

		AppBuilder _appBuilder;
		public AppBuilder CreateAppBuilder() => _appBuilder ??= App.CreateDefaultBuilder();

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			_page = FindViewById<ViewGroup>(Resource.Id.pageLayout);

			var app = CreateAppBuilder()
						.ConfigureServices(ConfigureExtraServices)
						//.RegisterHandlers(new Dictionary<Type, Type> 
						//{
						//	{ typeof(Xamarin.Platform.VerticalStackLayout),typeof(LayoutHandler) },
						//	{ typeof(Xamarin.Platform.HorizontalStackLayout),typeof(LayoutHandler) },
						//	{ typeof(Xamarin.Forms.FlexLayout),typeof(LayoutHandler) },
						//	{ typeof(Xamarin.Forms.StackLayout),typeof(LayoutHandler) },
						//})
						.Init<MyApp>();
			
			_appBuilder.Start();

			var page = app.Services.GetRequiredService<IStartup>() as Pages.MainPage;
			Add(page.GetContentView());
		}
		
		void ConfigureExtraServices(HostBuilderContext ctx, IServiceCollection services)
		{
			services.AddSingleton<ITextService, Services.DroidTextService>();
		}

		void Add(params IView[] views)
		{
			foreach (var view in views)
			{
				_page.AddView(view.ToNative(this), new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
			}
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}