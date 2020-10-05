namespace Xamarin.Platform.Handlers
{
    public partial class PathHandler : AbstractViewHandler<IPath, NativePath>
    {
        protected override NativePath CreateView()
        {
            return new NativePath();
        }
    }
}