using Xamarin.Platform.Handlers;
using RegistrarHandlers = Xamarin.Platform.Registrar;

namespace Sample
{
	public class Platform
	{
		static bool HasInit;

		public static void Init()
		{
			if (HasInit)
				return;

			HasInit = true;

			RegistrarHandlers.Handlers.Register<Button, ButtonHandler>();
			RegistrarHandlers.Handlers.Register<Ellipse, EllipseHandler>();
			RegistrarHandlers.Handlers.Register<Line, LineHandler>();
			RegistrarHandlers.Handlers.Register<Polygon, PolygonHandler>();
			RegistrarHandlers.Handlers.Register<Polyline, PolylineHandler>();
			RegistrarHandlers.Handlers.Register<Rectangle, RectangleHandler>();
		}
	}
}