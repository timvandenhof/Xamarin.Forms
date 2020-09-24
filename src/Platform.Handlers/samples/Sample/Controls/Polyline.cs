using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
    public class Polyline : Shape, IPolyline
    {
        public Polyline()
        {

        }

        public PointCollection Points { get; set; }

        public FillRule FillRule { get; set; } = FillRule.EvenOdd;
    }
}