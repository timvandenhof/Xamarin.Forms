using Android.Views;
using ATextAlignment = Android.Views.TextAlignment;

namespace Xamarin.Platform
{
	public static class AlignmentExtensions
	{
		public static ATextAlignment ToTextAlignment(this Forms.TextAlignment alignment)
		{
			switch (alignment)
			{
				case Forms.TextAlignment.Center:
					return ATextAlignment.Center;
				case Forms.TextAlignment.End:
					return ATextAlignment.ViewEnd;
				default:
					return ATextAlignment.ViewStart;
			}
		}

		public static GravityFlags ToHorizontalGravityFlags(this Forms.TextAlignment alignment)
		{
			switch (alignment)
			{
				case Forms.TextAlignment.Center:
					return GravityFlags.CenterHorizontal;
				case Forms.TextAlignment.End:
					return GravityFlags.End;
				default:
					return GravityFlags.Start;
			}
		}

		public static GravityFlags ToVerticalGravityFlags(this Forms.TextAlignment alignment)
		{
			switch (alignment)
			{
				case Forms.TextAlignment.Start:
					return GravityFlags.Top;
				case Forms.TextAlignment.End:
					return GravityFlags.Bottom;
				default:
					return GravityFlags.CenterVertical;
			}
		}
	}
}