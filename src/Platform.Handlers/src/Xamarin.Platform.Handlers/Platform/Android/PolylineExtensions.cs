using Xamarin.Forms;

namespace Xamarin.Platform
{
	public static class PolylineExtensions
	{
		public static void UpdatePoints(this NativePolyline nativePolyline, IPolyline polyline)
		{
			nativePolyline.UpdatePoints(polyline.Points);
		}

		public static void UpdateFillRule(this NativePolyline nativePolyline, IPolyline polyline)
		{
			nativePolyline.UpdateFillMode(polyline.FillRule == FillRule.Nonzero);
		}
	}
}