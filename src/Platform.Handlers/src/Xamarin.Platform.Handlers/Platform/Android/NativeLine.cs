using System;
using Android.Content;
using APath = Android.Graphics.Path;

namespace Xamarin.Platform
{
    public class NativeLine : NativeShape
    {
        float _x1, _y1, _x2, _y2;

        public NativeLine(Context? context) : base(context)
        {
        }

        void UpdateShape()
        {
            var path = new APath();
            path.MoveTo(_x1, _y1);
            path.LineTo(_x2, _y2);
            UpdateShape(path);
        }

        public void UpdateX1(float x1)
        {
            _x1 = _density * x1;
            UpdateShape();
        }

        public void UpdateY1(float y1)
        {
            _y1 = _density * y1;
            UpdateShape();
        }

        public void UpdateX2(float x2)
        {
            _x2 = _density * x2;
            UpdateShape();
        }

        public void UpdateY2(float y2)
        {
            _y2 = _density * y2;
            UpdateShape();
        }
    }
}