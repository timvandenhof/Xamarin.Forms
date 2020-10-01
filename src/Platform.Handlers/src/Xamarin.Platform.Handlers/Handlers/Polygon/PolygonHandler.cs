namespace Xamarin.Platform.Handlers
{
    public partial class PolygonHandler
	{
		public static PropertyMapper<IPolygon, PolygonHandler> PolygonMapper = new PropertyMapper<IPolygon, PolygonHandler>(ShapeHandler.ShapeMapper)
		{
			[nameof(IPolygon.Points)] = MapPoints,
			[nameof(IPolygon.FillRule)] = MapFillRule
		};

		public static void MapPoints(PolygonHandler handler, IPolygon polygon)
		{
			handler.TypedNativeView?.UpdatePoints(polygon);
		}

		public static void MapFillRule(PolygonHandler handler, IPolygon polygon)
		{
			handler.TypedNativeView?.UpdateFillRule(polygon);
		}

		public PolygonHandler() : base(PolygonMapper)
		{

		}

		public PolygonHandler(PropertyMapper mapper) : base(mapper ?? PolygonMapper)
		{

		}
	}
}