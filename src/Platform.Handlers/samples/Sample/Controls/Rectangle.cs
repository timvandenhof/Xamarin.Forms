using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
    public class Rectangle : Shape, IRectangle
    {
        public Rectangle()
        {
            Aspect = Stretch.Fill;
        }

        public double RadiusX { get; set; }

        public double RadiusY { get; set; }
    }
}
