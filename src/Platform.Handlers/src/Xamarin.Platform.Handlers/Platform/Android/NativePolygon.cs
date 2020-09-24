using Android.Content;
using Xamarin.Forms;
using static Android.Graphics.Path;
using APath = Android.Graphics.Path;

namespace Xamarin.Platform
{
    public class NativePolygon : NativeShape
    {
        PointCollection _points;
        bool _fillMode;

        public NativePolygon(Context context) : base(context)
        {

        }

        void UpdateShape()
        {
            if (_points != null && _points.Count > 1)
            {
                APath path = new APath();
                path.SetFillType(_fillMode ? FillType.Winding : FillType.EvenOdd);

                path.MoveTo(_density * (float)_points[0].X, _density * (float)_points[0].Y);

                for (int index = 1; index < _points.Count; index++)
                    path.LineTo(_density * (float)_points[index].X, _density * (float)_points[index].Y);

                path.Close();

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
