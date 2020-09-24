using Xamarin.Forms;

namespace Xamarin.Platform
{
    public interface IShape : IView
    {
        Color Fill { get; }
        Color Stroke { get; }
        double StrokeThickness { get; set; }
        DoubleCollection StrokeDashArray { get; }
        double StrokeDashOffset { get; }
        PenLineCap StrokeLineCap { get; set; }
        PenLineJoin StrokeLineJoin { get; }
        double StrokeMiterLimit { get; }
        Stretch Aspect { get; set; }
    }
}