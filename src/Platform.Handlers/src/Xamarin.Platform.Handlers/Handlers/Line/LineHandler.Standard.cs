using System;

namespace Xamarin.Platform.Handlers
{
    public partial class LineHandler : AbstractViewHandler<ILine, object>
	{
		public static void MapPropertyX1(IViewHandler handler, ILine view) { }
		public static void MapPropertyY1(IViewHandler handler, ILine view) { }
		public static void MapPropertyX2(IViewHandler handler, ILine view) { }
		public static void MapPropertyY2(IViewHandler handler, ILine view) { }

		protected override object CreateView() => throw new NotImplementedException();
	}
}