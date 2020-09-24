using Xamarin.Forms;

namespace Xamarin.Platform.Handlers
{
    public partial class PolylineHandler : AbstractViewHandler<IPolyline, NativePolyline>
    {
        protected override NativePolyline CreateView()
        {
            return new NativePolyline(Context);
        }

        public static void MapPropertyPoints(IViewHandler handler, IPolyline view)
        {
            (handler as PolylineHandler)?.UpdatePoints();
        }

        public static void MapPropertyFillRule(IViewHandler handler, IPolyline view)
        {
            (handler as PolylineHandler)?.UpdateFillRule();
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