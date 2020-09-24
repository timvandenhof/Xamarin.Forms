using System;

namespace Xamarin.Platform.Handlers
{
    public partial class PolylineHandler : AbstractViewHandler<IPolyline, object>
	{
		public static void MapPropertyPoints(IViewHandler handler, IPolyline view) { }
		public static void MapPropertyFillRule(IViewHandler handler, IPolyline view) { }

		protected override object CreateView() => throw new NotImplementedException();
	}
}