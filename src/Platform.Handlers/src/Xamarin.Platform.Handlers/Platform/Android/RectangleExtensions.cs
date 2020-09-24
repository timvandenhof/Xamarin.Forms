namespace Xamarin.Platform
{
	public static class RectangleExtensions
	{
		public static void UpdateRadiusX(this NativeRectangle nativeRectangle, IRectangle rectangle)
		{
			nativeRectangle.UpdateRadiusX(rectangle.RadiusX / rectangle.Frame.Width);
		}

		public static void UpdateRadiusY(this NativeRectangle nativeRectangle, IRectangle rectangle)
		{
			nativeRectangle.UpdateRadiusY(rectangle.RadiusY / rectangle.Frame.Height);
		}
	}
}