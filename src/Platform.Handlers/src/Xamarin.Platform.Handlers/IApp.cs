using Microsoft.Extensions.Hosting;
using System;

namespace Xamarin.Platform.Core
{
	public interface IApp
	{
		IHost Host { get; }

		IServiceProvider Services { get; }
		
		IView CreateView();
	}
}