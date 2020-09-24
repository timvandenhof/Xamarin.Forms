namespace Xamarin.Platform.Handlers
{
	public partial class EllipseHandler
	{
		public static PropertyMapper<IEllipse> EllipseMapper = new PropertyMapper<IEllipse>(ShapeHandler.ShapeMapper);

		public EllipseHandler() : base(EllipseMapper)
		{

		}

		public EllipseHandler(PropertyMapper mapper) : base(mapper ?? EllipseMapper)
		{

		}
	}
}