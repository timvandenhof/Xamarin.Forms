namespace Xamarin.Forms
{
    public class ArcSegment : PathSegment
    {
        public ArcSegment()
        {

        }

        public ArcSegment(Point point, Size size, double rotationAngle, SweepDirection sweepDirection, bool isLargeArc)
        {
            Point = point;
            Size = size;
            RotationAngle = rotationAngle;
            SweepDirection = sweepDirection;
            IsLargeArc = isLargeArc;
        }

        public Point Point { get; set; }

        public Size Size { get; set; }

        public double RotationAngle { get; set; }

        public SweepDirection SweepDirection { get; set; } = SweepDirection.CounterClockwise;

        public bool IsLargeArc { get; set; }
    }
}