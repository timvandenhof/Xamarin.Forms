using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
    public class Shape : View, IShape
    {
        public Shape()
        {
        }

        public Color Fill { get; set; }

        public Color Stroke { get; set; }

        public double StrokeThickness { get; set; }

        public DoubleCollection StrokeDashArray { get; set; }

        public double StrokeDashOffset { get; set; }

        public PenLineCap StrokeLineCap { get; set; }

        public PenLineJoin StrokeLineJoin { get; set; }

        public double StrokeMiterLimit { get; set; }

        public Stretch Aspect { get; set; }
    }
}
