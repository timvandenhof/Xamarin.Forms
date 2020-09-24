using System;

namespace Xamarin.Platform.Handlers
{
    public partial class RectangleHandler : AbstractViewHandler<IRectangle, object>
	{
		public static void MapPropertyRadiusX(IViewHandler handler, IRectangle rectangle) { }
		public static void MapPropertyRadiusY(IViewHandler handler, IRectangle rectangle) { }

		protected override object CreateView() => throw new NotImplementedException();
	}
}