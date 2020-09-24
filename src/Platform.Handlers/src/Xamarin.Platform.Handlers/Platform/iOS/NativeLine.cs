using System;
using CoreGraphics;

namespace Xamarin.Platform
{
    public class NativeLine : NativeShape
    {
        nfloat _x1, _y1, _x2, _y2;

        public void UpdateX1(double x1)
        {
            _x1 = new nfloat(x1);
            UpdateShape();
        }

        public void UpdateY1(double y1)
        {
            _y1 = new nfloat(y1);
            UpdateShape();
        }

        public void UpdateX2(double x2)
        {
            _x2 = new nfloat(x2);
            UpdateShape();
        }

        public void UpdateY2(double y2)
        {
            _y2 = new nfloat(y2);
            UpdateShape();
        }

        void UpdateShape()
        {
            var path = new CGPath();
            path.MoveToPoint(_x1, _y1);
            path.AddLineToPoint(_x2, _y2);
            ShapeLayer.UpdateShape(path);
        }
    }
}