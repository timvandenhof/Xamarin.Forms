namespace Xamarin.Platform
{
	public static class UnitExtensions
	{
		public static float ToEm(this double pt)
		{
			return (float)pt * 0.0624f; // Coefficient for converting Pt to Em
		}
	}
}
