namespace Xamarin.Platform.Handlers
{
    public partial class LineHandler : AbstractViewHandler<ILine, NativeLine>
    {
        protected override NativeLine CreateView()
        {
            return new NativeLine(Context);
        }

        public static void MapPropertyX1(IViewHandler handler, ILine view)
        {
            (handler as LineHandler)?.UpdateX1();
        }

        public static void MapPropertyY1(IViewHandler handler, ILine view)
        {
            (handler as LineHandler)?.UpdateY1();
        }

        public static void MapPropertyX2(IViewHandler handler, ILine view)
        {
            (handler as LineHandler)?.UpdateX2();
        }

        public static void MapPropertyY2(IViewHandler handler, ILine view)
        {
            (handler as LineHandler)?.UpdateY2();
        }

        public virtual void UpdateX1()
        {
            TypedNativeView.UpdateX1((float)VirtualView.X1);
        }

        public virtual void UpdateY1()
        {
            TypedNativeView.UpdateY1((float)VirtualView.Y1);
        }

        public virtual void UpdateX2()
        {
            TypedNativeView.UpdateX2((float)VirtualView.X2);
        }

        public virtual void UpdateY2()
        {
            TypedNativeView.UpdateY2((float)VirtualView.Y2);
        }
    }
}
