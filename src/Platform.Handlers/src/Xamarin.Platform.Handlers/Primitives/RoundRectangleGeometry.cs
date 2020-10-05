namespace Xamarin.Forms
{
	public class RoundRectangleGeometry : GeometryGroup
	{
		Rect _rect;
		CornerRadius _cornerRadius;

		public RoundRectangleGeometry()
		{

		}

		public RoundRectangleGeometry(CornerRadius cornerRadius, Rect rect)
		{
			CornerRadius = cornerRadius;
			Rect = rect;
		}

		public Rect Rect
		{
			get { return _rect; }
			set
			{
				_rect = value;
				UpdateGeometry();
			}
		}

		public CornerRadius CornerRadius
		{
			get { return _cornerRadius; }
			set
			{
				_cornerRadius = value;
				UpdateGeometry();
			}
		}

		void UpdateGeometry()
		{
			FillRule = FillRule.Nonzero;

			Children.Clear();

			Children.Add(GetRoundRectangleGeometry());
		}

		Geometry GetRoundRectangleGeometry()
		{
			GeometryGroup roundedRectGeometry = new GeometryGroup
			{
				FillRule = FillRule.Nonzero
			};

			if (CornerRadius.TopLeft > 0)
				roundedRectGeometry.Children.Add(
					new EllipseGeometry(new Point(Rect.Location.X + CornerRadius.TopLeft, Rect.Location.Y + CornerRadius.TopLeft), Rect.Location.Y + CornerRadius.TopLeft, Rect.Location.Y + CornerRadius.TopLeft));

			if (CornerRadius.TopRight > 0)
				roundedRectGeometry.Children.Add(
					new EllipseGeometry(new Point(Rect.Location.X + Rect.Width - CornerRadius.TopRight, Rect.Location.Y + CornerRadius.TopRight), CornerRadius.TopRight, CornerRadius.TopRight));

			if (CornerRadius.BottomRight > 0)
				roundedRectGeometry.Children.Add(
					new EllipseGeometry(new Point(Rect.Location.X + Rect.Width - CornerRadius.BottomRight, Rect.Location.Y + Rect.Height - CornerRadius.BottomRight), CornerRadius.BottomRight, CornerRadius.BottomRight));

			if (CornerRadius.BottomLeft > 0)
				roundedRectGeometry.Children.Add(
					new EllipseGeometry(new Point(Rect.Location.X + CornerRadius.BottomLeft, Rect.Location.Y + Rect.Height - CornerRadius.BottomLeft), CornerRadius.BottomLeft, CornerRadius.BottomLeft));

			PathFigure pathFigure = new PathFigure
			{
				IsClosed = true,
				StartPoint = new Point(Rect.Location.X + CornerRadius.TopLeft, Rect.Location.Y),
				Segments = new PathSegmentCollection
				{
					new LineSegment { Point = new Point(Rect.Location.X + Rect.Width - CornerRadius.TopRight, Rect.Location.Y) },
					new LineSegment { Point = new Point(Rect.Location.X + Rect.Width, Rect.Location.Y + CornerRadius.TopRight) },
					new LineSegment { Point = new Point(Rect.Location.X + Rect.Width, Rect.Location.Y + Rect.Height - CornerRadius.BottomRight) },
					new LineSegment { Point = new Point(Rect.Location.X + Rect.Width - CornerRadius.BottomRight, Rect.Location.Y + Rect.Height) },
					new LineSegment { Point = new Point(Rect.Location.X + CornerRadius.BottomLeft, Rect.Location.Y + Rect.Height) },
					new LineSegment { Point = new Point(Rect.Location.X, Rect.Location.Y + Rect.Height - CornerRadius.BottomLeft) },
					new LineSegment { Point = new Point(Rect.Location.X, Rect.Location.Y + CornerRadius.TopLeft) }
				}
			};

			PathFigureCollection pathFigureCollection = new PathFigureCollection
			{
				pathFigure
			};

			roundedRectGeometry.Children.Add(new PathGeometry(pathFigureCollection, FillRule.Nonzero));

			return roundedRectGeometry;
		}
	}
}