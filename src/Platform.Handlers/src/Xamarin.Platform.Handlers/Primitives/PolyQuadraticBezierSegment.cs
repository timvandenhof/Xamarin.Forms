namespace Xamarin.Forms
{
    public class PolyQuadraticBezierSegment : PathSegment
    {
        public PolyQuadraticBezierSegment()
        {
            Points = new PointCollection();
        }

        public PolyQuadraticBezierSegment(PointCollection points)
        {
            Points = points;
        }

        public PointCollection Points { get; set; }
    }
}