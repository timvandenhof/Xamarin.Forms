using Xamarin.Forms;

namespace Xamarin.Platform
{
    public static class DoubleCollectionExtensions
    {
        public static float[] ToNative(this DoubleCollection doubleCollection)
        {
            if (doubleCollection == null)
                return new float[0];

            float[] array = new float[doubleCollection.Count];

            for (int i = 0; i < doubleCollection.Count; i++)
            {
                array[i] = (float)doubleCollection[i];
            }

            return array;
        }
    }
}