namespace Xamarin.Forms
{
    public class LineSegment : PathSegment
    {
        public LineSegment()
        {

        }

        public LineSegment(Point point)
        {
            Point = point;
        }

        public Point Point { get; set; }
    }
}
