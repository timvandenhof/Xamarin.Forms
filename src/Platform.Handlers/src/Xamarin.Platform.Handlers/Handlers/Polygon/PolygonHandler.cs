namespace Xamarin.Platform.Handlers
{
    public partial class PolygonHandler
    {
		public static PropertyMapper<IPolygon> PolygonMapper = new PropertyMapper<IPolygon>(ShapeHandler.ShapeMapper)
		{
			[nameof(IPolygon.Points)] = MapPropertyPoints,
			[nameof(IPolygon.FillRule)] = MapPropertyFillRule
		};

		public PolygonHandler() : base(PolygonMapper)
		{

		}

		public PolygonHandler(PropertyMapper mapper) : base(mapper ?? PolygonMapper)
		{

		}
	}
}