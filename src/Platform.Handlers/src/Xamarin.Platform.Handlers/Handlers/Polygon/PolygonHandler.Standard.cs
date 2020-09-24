using System;

namespace Xamarin.Platform.Handlers
{
    public partial class PolygonHandler : AbstractViewHandler<IPolygon, object>
	{
		public static void MapPropertyPoints(IViewHandler handler, IPolygon view) { }
		public static void MapPropertyFillRule(IViewHandler handler, IPolygon view) { }

		protected override object CreateView() => throw new NotImplementedException();
	}
}