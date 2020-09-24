using Xamarin.Forms;

namespace Xamarin.Platform.Handlers
{
    public partial class PolygonHandler : AbstractViewHandler<IPolygon, NativePolygon>
    {
        protected override NativePolygon CreateView()
        {
            return new NativePolygon(Context);
        }

        public static void MapPropertyPoints(IViewHandler handler, IPolygon view)
        {
            (handler as PolygonHandler)?.UpdatePoints();
        }

        public static void MapPropertyFillRule(IViewHandler handler, IPolygon view)
        {
            (handler as PolygonHandler)?.UpdateFillRule();
        }

        public virtual void UpdatePoints()
        {
            TypedNativeView.UpdatePoints(VirtualView.Points);
        }

        public virtual void UpdateFillRule()
        {
            TypedNativeView.UpdateFillMode(VirtualView.FillRule == FillRule.Nonzero);
        }
    }
}