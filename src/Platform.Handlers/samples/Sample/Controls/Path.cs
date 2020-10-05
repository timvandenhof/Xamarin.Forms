using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
    public class Path : Shape, IPath
    {
        public Path()
        {

        }

        public Geometry Data { get; set; }

        public Transform RenderTransform { get; set; }
    }
}