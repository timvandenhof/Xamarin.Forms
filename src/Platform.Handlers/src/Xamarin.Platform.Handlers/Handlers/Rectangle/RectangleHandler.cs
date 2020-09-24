namespace Xamarin.Platform.Handlers
{
    public partial class RectangleHandler
    {
		public static PropertyMapper<IRectangle> RectangleMapper = new PropertyMapper<IRectangle>(ShapeHandler.ShapeMapper)
		{
			[nameof(IRectangle.RadiusX)] = MapPropertyRadiusX,
			[nameof(IRectangle.RadiusY)] = MapPropertyRadiusY
		};

		public RectangleHandler() : base(RectangleMapper)
		{

		}

		public RectangleHandler(PropertyMapper mapper) : base(mapper ?? RectangleMapper)
		{

		}
	}
}