using CoreGraphics;

namespace Xamarin.Platform
{
    public class PathData
    {
        public CGPath? Data { get; set; }
        public bool IsNonzeroFillRule { get; set; }
    }

    public class NativePath : NativeShape
    {
        public void UpdatePath(PathData path)
        {
            ShapeLayer.UpdateShape(path.Data);
            ShapeLayer.UpdateFillMode(path != null && path.IsNonzeroFillRule);
        }
    }
}