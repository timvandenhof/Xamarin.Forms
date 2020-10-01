using Xamarin.Forms;

namespace Xamarin.Platform.Handlers
{
	public partial class EllipseHandler : AbstractViewHandler<IEllipse, NativeEllipse>
	{
		protected override NativeEllipse CreateView() => new NativeEllipse();

		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			if (TypedNativeView != null)
			{
				return TypedNativeView.ShapeLayer.GetDesiredSize();
			}

			return base.GetDesiredSize(widthConstraint, heightConstraint);
		}
	}
}