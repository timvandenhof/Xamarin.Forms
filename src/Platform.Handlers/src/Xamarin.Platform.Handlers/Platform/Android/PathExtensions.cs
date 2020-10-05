namespace Xamarin.Platform
{
	public static class PathExtensions
	{
		public static void UpdateData(this NativePath nativePath, IPath path)
		{
			nativePath.UpdateData(path.Data.ToNative(nativePath.Context));
		}

		public static void UpdateRenderTransform(this NativePath nativePath, IPath path)
		{
			if (path.RenderTransform != null)
			{
				var density = nativePath?.Context?.Resources?.DisplayMetrics != null ? nativePath.Context.Resources.DisplayMetrics.Density : 1.0f;
				nativePath?.UpdateTransform(path.RenderTransform.ToNative(density));
			}
		}
	}
}