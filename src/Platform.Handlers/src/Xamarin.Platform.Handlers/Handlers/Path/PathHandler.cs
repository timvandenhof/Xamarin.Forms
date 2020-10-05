namespace Xamarin.Platform.Handlers
{
    public partial class PathHandler
	{
		public static PropertyMapper<IPath, PathHandler> PathMapper = new PropertyMapper<IPath, PathHandler>(ShapeHandler.ShapeMapper)
		{
			[nameof(IPath.Data)] = MapData,
			[nameof(IPath.RenderTransform)] = MapRenderTransform
		};

		public static void MapData(PathHandler handler, IPath path)
		{
			ViewHandler.CheckParameters(handler, path);
			handler.TypedNativeView?.UpdateData(path);
		}

		public static void MapRenderTransform(PathHandler handler, IPath path)
		{
			ViewHandler.CheckParameters(handler, path);
			handler.TypedNativeView?.UpdateRenderTransform(path);
		}

		public PathHandler() : base(PathMapper)
		{

		}

		public PathHandler(PropertyMapper mapper) : base(mapper ?? PathMapper)
		{

		}
	}
}