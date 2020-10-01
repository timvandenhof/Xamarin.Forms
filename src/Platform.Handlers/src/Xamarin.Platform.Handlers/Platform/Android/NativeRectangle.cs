using Android.Content;
using Android.Graphics;
using APath = Android.Graphics.Path;

namespace Xamarin.Platform
{
    public class NativeRectangle : NativeShape
    {
        public NativeRectangle(Context? context) : base(context)
        {
            UpdateShape();
        }

        public float RadiusX { set; get; }

        public float RadiusY { set; get; }

        void UpdateShape()
        {
            var path = new APath();
            path.AddRoundRect(new RectF(0, 0, 1, 1), RadiusX, RadiusY, APath.Direction.Cw!);
            UpdateShape(path);
        }

        public void UpdateRadiusX(double radiusX)
        {
            RadiusX = (float)radiusX;
            UpdateShape();
        }

        public void UpdateRadiusY(double radiusY)
        {
            RadiusY = (float)radiusY;
            UpdateShape();
        }
    }
}