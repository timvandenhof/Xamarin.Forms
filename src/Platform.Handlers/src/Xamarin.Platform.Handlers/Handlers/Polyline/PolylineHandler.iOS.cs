namespace Xamarin.Platform.Handlers
{
	public partial class PolylineHandler : AbstractViewHandler<IPolyline, NativePolyline>
	{
		protected override NativePolyline CreateView() => new NativePolyline();
	}
}