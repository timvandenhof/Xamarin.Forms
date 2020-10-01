namespace Xamarin.Platform.Handlers
{
    public partial class RectangleHandler 
	{
		public static PropertyMapper<IRectangle, RectangleHandler> RectangleMapper = new PropertyMapper<IRectangle, RectangleHandler>(ShapeHandler.ShapeMapper)
		{
			[nameof(IRectangle.RadiusX)] = MapRadiusX,
			[nameof(IRectangle.RadiusY)] = MapRadiusY
		};

		public static void MapRadiusX(RectangleHandler handler, IRectangle rectangle)
		{
			handler.TypedNativeView?.UpdateRadiusX(rectangle);
		}

		public static void MapRadiusY(RectangleHandler handler, IRectangle rectangle)
		{
			handler.TypedNativeView?.UpdateRadiusY(rectangle);
		}

		public RectangleHandler() : base(RectangleMapper)
		{

		}

		public RectangleHandler(PropertyMapper mapper) : base(mapper ?? RectangleMapper)
		{

		}
	}
}