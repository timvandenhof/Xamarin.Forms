using Android.Content;
using AMatrix = Android.Graphics.Matrix;
using APath = Android.Graphics.Path;

namespace Xamarin.Platform
{
    public class NativePath : NativeShape
    {
        public NativePath(Context context) : base(context)
        {
        }

        public void UpdateData(APath path)
        {
            UpdateShape(path);
        }

        public void UpdateTransform(AMatrix transform)
        {
            UpdateShapeTransform(transform);
        }
    }
}