using Android.Content;
using Xamarin.Forms;
using static Android.Graphics.Path;
using APath = Android.Graphics.Path;

namespace Xamarin.Platform
{
    public class NativePolyline : NativeShape
    {
        PointCollection? _points;
        bool _fillMode;

        public NativePolyline(Context? context) : base(context)
        {
        }

        void UpdateShape()
        {
            if (_points != null && _points.Count > 1)
            {
                var path = new APath();
                path.SetFillType(_fillMode ? FillType.Winding! : FillType.EvenOdd!);

                path.MoveTo(_density * (float)_points[0].X, _density * (float)_points[0].Y);

                for (int index = 1; index < _points.Count; index++)
                    path.LineTo(_density * (float)_points[index].X, _density * (float)_points[index].Y);

                UpdateShape(path);
            }
        }

        public void UpdatePoints(PointCollection points)
        {
            _points = points;
            UpdateShape();
        }

        public void UpdateFillMode(bool fillMode)
        {
            _fillMode = fillMode;
            UpdateShape();
        }
    }
}