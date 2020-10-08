using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Xamarin.Platform.Hosting
{
	public static class ServiceProviderExtensions
	{
		public static IViewHandler? GetHandler(this IServiceProvider services, Type type)
		{
			var handler = GetHandler(type, services);
			return handler;
		}

		static IViewHandler GetHandler(Type type, IServiceProvider services)
		{
			List<Type> types = new List<Type> { type };
			foreach (var interfac in type.GetInterfaces())
			{
				if (typeof(IView).IsAssignableFrom(interfac))
					types.Add(interfac);

			}
			Type baseType = type.BaseType;

			while (baseType != null)
			{
				//if (typeof(IView).IsAssignableFrom(baseType) &&
				//	baseType.FullName != "System.Object")
				types.Add(baseType);

				baseType = baseType.BaseType;
			}

			foreach (var t in types)
			{
				var generic = typeof(HostBuilderExtensions.IRenderer<>).MakeGenericType(t);
				var handlersForType = services.GetServices(generic);
				var handlerForType = handlersForType.LastOrDefault();
				var serviceGenerics = handlerForType?.GetType().GetGenericArguments();
				if (serviceGenerics?.Length > 1)
				{
					var newObject = Activator.CreateInstance(serviceGenerics[1]);
					return (IViewHandler)newObject;
				}
			}

			return default!;
		}
	}
}
