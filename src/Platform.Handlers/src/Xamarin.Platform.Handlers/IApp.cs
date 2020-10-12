using Microsoft.Extensions.Hosting;
using System;
using Xamarin.Platform.Hosting;

namespace Xamarin.Platform.Core
{
	public interface IApp
	{
		IHost Host { get; }

		IServiceProvider Services { get; }

		IHandlerServiceProvider Handlers { get; }

		IView CreateView();
	}
}