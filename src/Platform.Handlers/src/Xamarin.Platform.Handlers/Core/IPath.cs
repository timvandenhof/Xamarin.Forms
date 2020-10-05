using Xamarin.Forms;

namespace Xamarin.Platform
{
    public interface IPath : IShape
    {
        Geometry Data { get; }
        Transform RenderTransform { get; }
    }
}