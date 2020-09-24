using Xamarin.Forms;

namespace Xamarin.Platform
{
    public interface IPolygon : IShape
    {
        PointCollection Points { get; }
        FillRule FillRule { get; }
    }
}
