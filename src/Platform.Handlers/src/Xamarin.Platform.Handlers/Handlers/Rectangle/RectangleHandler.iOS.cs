namespace Xamarin.Platform.Handlers
{
    public partial class RectangleHandler : AbstractViewHandler<IRectangle, NativeRectangle>
    {
        protected override NativeRectangle CreateView()
        {
            return new NativeRectangle();
        }

        public static void MapPropertyRadiusX(IViewHandler handler, IRectangle rectangle)
        {
            (handler as RectangleHandler)?.UpdateRadiusX();
        }

        public static void MapPropertyRadiusY(IViewHandler handler, IRectangle rectangle)
        {
            (handler as RectangleHandler)?.UpdateRadiusY();
        }

        public virtual void UpdateRadiusX()
        {
            TypedNativeView.UpdateRadiusX(VirtualView.RadiusX / VirtualView.Frame.Width);
        }

        public virtual void UpdateRadiusY()
        {
            TypedNativeView.UpdateRadiusY(VirtualView.RadiusY / VirtualView.Frame.Height);
        }
    }
}