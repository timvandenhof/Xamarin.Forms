using Xamarin.Forms;

namespace Xamarin.Platform
{
	public static class PolygonExtensions
	{
		public static void UpdatePoints(this NativePolygon nativePolygon, IPolygon polygon)
		{
			nativePolygon.UpdatePoints(polygon.Points.ToNative());
		}

		public static void UpdateFillRule(this NativePolygon nativePolygon, IPolygon polygon)
		{
			nativePolygon.UpdateFillMode(polygon.FillRule == FillRule.Nonzero);
		}
	}
}