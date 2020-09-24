using Xamarin.Forms;

namespace Xamarin.Platform
{ 
    public interface IPolyline : IShape
    {
        PointCollection Points { get; }
        FillRule FillRule { get; }
    }
}