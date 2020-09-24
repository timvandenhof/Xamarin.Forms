#if NETSTANDARD
using NativeView = System.Object;
#else
using NativeView = Xamarin.Platform.NativePolyline;
# endif

namespace Xamarin.Platform.Handlers
{
    public partial class PolylineHandler : AbstractViewHandler<IPolyline, NativeView>
	{
		public static PropertyMapper<IPolyline, PolylineHandler> PolylineMapper = new PropertyMapper<IPolyline, PolylineHandler>(ShapeHandler.ShapeMapper)
		{
			[nameof(IPolyline.Points)] = MapPoints,
			[nameof(IPolyline.FillRule)] = MapFillRule
		};

		public static void MapPoints(PolylineHandler handler, IPolyline polyline)
		{
			handler.TypedNativeView.UpdatePoints(polyline);
		}

		public static void MapFillRule(PolylineHandler handler, IPolyline polyline)
		{
			handler.TypedNativeView.UpdateFillRule(polyline);
		}

#if MONOANDROID
		protected override NativeView CreateView() => new NativeView(Context);
#else
		protected override NativeView CreateView() => new NativeView();
#endif

		public PolylineHandler() : base(PolylineMapper)
		{

		}

		public PolylineHandler(PropertyMapper mapper) : base(mapper ?? PolylineMapper)
		{

		}
	}
}