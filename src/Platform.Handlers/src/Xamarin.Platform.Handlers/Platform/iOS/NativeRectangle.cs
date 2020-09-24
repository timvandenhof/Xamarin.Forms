using System;
using CoreGraphics;

namespace Xamarin.Platform
{
    public class NativeRectangle : NativeShape
    {
        public NativeRectangle()
        {
            UpdateShape();
        }

        public nfloat RadiusX { set; get; }

        public nfloat RadiusY { set; get; }

        void UpdateShape()
        {
            var path = new CGPath();
            path.AddRoundedRect(new CGRect(0, 0, 1, 1), RadiusX, RadiusY);
            ShapeLayer.UpdateShape(path);
        }

        public void UpdateRadiusX(double radiusX)
        {
            if (double.IsInfinity(radiusX))
                return;

            RadiusX = new nfloat(radiusX);
            UpdateShape();
        }

        public void UpdateRadiusY(double radiusY)
        {
            if (double.IsInfinity(radiusY))
                return;

            RadiusY = new nfloat(radiusY);
            UpdateShape();
        }
    }
}