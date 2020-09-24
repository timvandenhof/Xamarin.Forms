#if NETSTANDARD
using NativeView = System.Object;
#else
using NativeView = Xamarin.Platform.NativeRectangle;
# endif

namespace Xamarin.Platform.Handlers
{
    public partial class RectangleHandler : AbstractViewHandler<IPolyline, NativeView>
	{
		public static PropertyMapper<IRectangle, RectangleHandler> RectangleMapper = new PropertyMapper<IRectangle, RectangleHandler>(ShapeHandler.ShapeMapper)
		{
			[nameof(IRectangle.RadiusX)] = MapRadiusX,
			[nameof(IRectangle.RadiusY)] = MapRadiusY
		};

		public static void MapRadiusX(RectangleHandler handler, IRectangle rectangle)
		{
			handler.TypedNativeView.UpdateRadiusX(rectangle);
		}

		public static void MapRadiusY(RectangleHandler handler, IRectangle rectangle)
		{
			handler.TypedNativeView.UpdateRadiusY(rectangle);
		}

#if MONOANDROID
		protected override NativeView CreateView() => new NativeView(Context);
#else
		protected override NativeView CreateView() => new NativeView();
#endif

		public RectangleHandler() : base(RectangleMapper)
		{

		}

		public RectangleHandler(PropertyMapper mapper) : base(mapper ?? RectangleMapper)
		{

		}
	}
}