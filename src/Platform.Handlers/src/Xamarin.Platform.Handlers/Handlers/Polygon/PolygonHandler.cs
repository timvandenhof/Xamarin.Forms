#if NETSTANDARD
using NativeView = System.Object;
#else
using NativeView = Xamarin.Platform.NativePolygon;
#endif

namespace Xamarin.Platform.Handlers
{
    public partial class PolygonHandler : AbstractViewHandler<IPolygon, NativeView>
	{
		public static PropertyMapper<IPolygon, PolygonHandler> PolygonMapper = new PropertyMapper<IPolygon, PolygonHandler>(ShapeHandler.ShapeMapper)
		{
			[nameof(IPolygon.Points)] = MapPoints,
			[nameof(IPolygon.FillRule)] = MapFillRule
		};

		public static void MapPoints(PolygonHandler handler, IPolygon polygon)
		{
			handler.TypedNativeView.UpdatePoints(polygon);
		}

		public static void MapFillRule(PolygonHandler handler, IPolygon polygon)
		{
			handler.TypedNativeView.UpdateFillRule(polygon);
		}

#if MONOANDROID
		protected override NativeView CreateView() => new NativeView(Context);
#else
		protected override NativeView CreateView() => new NativeView();
#endif

		public PolygonHandler() : base(PolygonMapper)
		{

		}

		public PolygonHandler(PropertyMapper mapper) : base(mapper ?? PolygonMapper)
		{

		}
	}
}