namespace Xamarin.Forms
{
    public class PathFigure
    {
        public PathFigure()
        {
            Segments = new PathSegmentCollection();
        }

        public PathSegmentCollection Segments { get; set; }
        public Point StartPoint { get; set; }
        public bool IsClosed { get; set; }
        public bool IsFilled { get; set; } = true;
    }
}