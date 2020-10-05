namespace Xamarin.Platform
{
	public static class PathExtensions
	{
		public static void UpdateData(this NativePath nativePath, IPath path)
		{
			nativePath.UpdatePath(path);
		}

		public static void UpdateRenderTransform(this NativePath nativePath, IPath path)
		{
			nativePath.UpdatePath(path);
		}

		internal static void UpdatePath(this NativePath nativePath, IPath path)
		{
			nativePath.UpdatePath(path.Data.ToNative(path.RenderTransform));
		}
	}
}