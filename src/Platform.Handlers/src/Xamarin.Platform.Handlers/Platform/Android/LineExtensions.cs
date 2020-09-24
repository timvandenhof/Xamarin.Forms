namespace Xamarin.Platform
{
	public static class LineExtensions
	{
		public static void UpdateX1(this NativeLine nativeLine, ILine line)
		{
			nativeLine.UpdateX1((float)line.X1);
		}

		public static void UpdateY1(this NativeLine nativeLine, ILine line)
		{
			nativeLine.UpdateX1((float)line.Y1);
		}

		public static void UpdateX2(this NativeLine nativeLine, ILine line)
		{
			nativeLine.UpdateX1((float)line.X2);
		}

		public static void UpdateY2(this NativeLine nativeLine, ILine line)
		{
			nativeLine.UpdateX1((float)line.Y2);
		}
	}
}
