namespace Xamarin.Forms
{
    public class GeometryGroup : Geometry
    {
        public GeometryGroup()
        {
            Children = new GeometryCollection();
        }

        public GeometryCollection Children { get; set; }

        public FillRule FillRule { get; set; }
    }
}
