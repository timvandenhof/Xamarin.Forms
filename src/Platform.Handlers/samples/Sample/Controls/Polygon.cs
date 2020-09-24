using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
    public class Polygon : Shape, IPolygon
    {
        public Polygon()
        {

        }

        public PointCollection Points { get; set; }

        public FillRule FillRule { get; set; }
    }
}
