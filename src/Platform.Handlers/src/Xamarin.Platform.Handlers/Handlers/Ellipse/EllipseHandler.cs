namespace Xamarin.Platform.Handlers
{
	public partial class EllipseHandler
	{
		public static PropertyMapper<IEllipse, EllipseHandler> EllipseMapper = new PropertyMapper<IEllipse, EllipseHandler>(ShapeHandler.ShapeMapper);

		public EllipseHandler() : base(EllipseMapper)
		{

		}

		public EllipseHandler(PropertyMapper mapper) : base(mapper ?? EllipseMapper)
		{

		}
	}
}