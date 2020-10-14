using AppKit;

namespace Xamarin.Platform
{
	public static class ViewExtensions
	{
		public static void UpdateIsEnabled(this NSView nativeView, IView view)
		{

		}

		public static void UpdateBackgroundColor(this NSView nativeView, IView view)
		{
			if (nativeView == null)
				return;

			nativeView.WantsLayer = true;

			if (nativeView.Layer != null)
			{
				var color = view.BackgroundColor;
				nativeView.Layer.BackgroundColor = color.ToCGColor();
			}
		}

		public static CoreGraphics.CGSize SizeThatFits(this NSView view, CoreGraphics.CGSize size) =>
			(view as NSControl)?.SizeThatFits(size) ?? view.FittingSize;
	}
}