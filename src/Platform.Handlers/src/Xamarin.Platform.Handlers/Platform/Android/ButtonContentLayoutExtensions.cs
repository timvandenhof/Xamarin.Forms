using System;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public static class ButtonContentLayoutExtensions
	{
		public static bool IsHorizontal(this ButtonContentLayout layout) =>
			layout.Position == ButtonContentLayout.ImagePosition.Left ||
			layout.Position == ButtonContentLayout.ImagePosition.Right;
	}
}
