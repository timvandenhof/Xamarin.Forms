using Xamarin.Forms;

#if NETSTANDARD
using NativeView = System.Object;
#else
using NativeView = Xamarin.Platform.NativeEllipse;
#endif

namespace Xamarin.Platform.Handlers
{
	public partial class EllipseHandler : AbstractViewHandler<IEllipse, NativeView>
	{
		public static PropertyMapper<IEllipse, EllipseHandler> EllipseMapper = new PropertyMapper<IEllipse, EllipseHandler>(ShapeHandler.ShapeMapper);

#if MONOANDROID
		protected override NativeView CreateView() => new NativeView(Context);
#else
		protected override NativeView CreateView() => new NativeView();
#endif

		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			if (TypedNativeView != null)
			{
#if __IOS__
				return TypedNativeView.ShapeLayer.GetDesiredSize();
#elif MONOANDROID
				return TypedNativeView.GetDesiredSize();
#endif
			}

			return base.GetDesiredSize(widthConstraint, heightConstraint);
		}

		public EllipseHandler() : base(EllipseMapper)
		{

		}

		public EllipseHandler(PropertyMapper mapper) : base(mapper ?? EllipseMapper)
		{

		}
	}
}