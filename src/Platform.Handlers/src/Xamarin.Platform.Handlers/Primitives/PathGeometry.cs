namespace Xamarin.Forms
{
    public class PathGeometry : Geometry
    {
        public PathGeometry()
        {
            Figures = new PathFigureCollection();
        }

        public PathGeometry(PathFigureCollection figures, FillRule fillRule)
        {
            Figures = figures;
            FillRule = fillRule;
        }

        public PathFigureCollection Figures { get; set; }

        public FillRule FillRule { get; set; } = FillRule.EvenOdd;
    }
}
