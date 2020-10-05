using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms;
using APath = Android.Graphics.Path;
using Point = Xamarin.Forms.Point;

namespace Xamarin.Platform
{
    public static class GeometryExtensions
    {
        public static APath ToNative(this Geometry geometry, Context? context)
        {
            APath path = new APath();

            float density = context?.Resources?.DisplayMetrics != null ? context.Resources.DisplayMetrics.Density : 1.0f;

            if (geometry is LineGeometry)
            {
                if (geometry is LineGeometry lineGeometry)
                {
                    path.MoveTo(
                        density * (float)lineGeometry.StartPoint.X,
                        density * (float)lineGeometry.StartPoint.Y);

                    path.LineTo(
                        density * (float)lineGeometry.EndPoint.X,
                        density * (float)lineGeometry.EndPoint.Y);
                }
            }
            else if (geometry is RectangleGeometry)
            {
				Forms.Rect? rect = (geometry as RectangleGeometry)?.Rect;

				if (rect != null && rect.HasValue)
                {
                    path.AddRect(
                        density * (float)rect.Value.Left,
                        density * (float)rect.Value.Top,
                        density * (float)rect.Value.Right,
                        density * (float)rect.Value.Bottom,
                        APath.Direction.Cw!);
                }
            }
            else if (geometry is EllipseGeometry)
            {
                if (geometry is EllipseGeometry ellipseGeometry)
                {
                    path.AddOval(new RectF(
                        density * (float)(ellipseGeometry.Center.X - ellipseGeometry.RadiusX),
                        density * (float)(ellipseGeometry.Center.Y - ellipseGeometry.RadiusY),
                        density * (float)(ellipseGeometry.Center.X + ellipseGeometry.RadiusX),
                        density * (float)(ellipseGeometry.Center.Y + ellipseGeometry.RadiusY)),
                        APath.Direction.Cw!);
                }
            }
            else if (geometry is GeometryGroup)
            {
                GeometryGroup? geometryGroup = geometry as GeometryGroup;

                path.SetFillType(geometryGroup?.FillRule == FillRule.Nonzero ? APath.FillType.Winding! : APath.FillType.EvenOdd!);

                if (geometryGroup != null)
                    foreach (Geometry child in geometryGroup.Children)
                    {
                        APath childPath = child.ToNative(context);
                        path.AddPath(childPath);
                    }
            }
            else if (geometry is PathGeometry)
            {
                PathGeometry? pathGeometry = geometry as PathGeometry;

                path.SetFillType(pathGeometry?.FillRule == FillRule.Nonzero ? APath.FillType.Winding! : APath.FillType.EvenOdd!);

                if (pathGeometry != null)
                    foreach (PathFigure pathFigure in pathGeometry.Figures)
                    {
                        path.MoveTo(
                            density * (float)pathFigure.StartPoint.X,
                            density * (float)pathFigure.StartPoint.Y);

                        Point lastPoint = pathFigure.StartPoint;

                        foreach (PathSegment pathSegment in pathFigure.Segments)
                        {
                            // LineSegment
                            if (pathSegment is LineSegment)
                            {
                                if (pathSegment is LineSegment lineSegment)
                                {
                                    path.LineTo(
                                        density * (float)lineSegment.Point.X,
                                        density * (float)lineSegment.Point.Y);
                                    lastPoint = lineSegment.Point;
                                }
                            }
                            // PolylineSegment
                            else if (pathSegment is PolyLineSegment)
                            {
                                PointCollection points = pathSegment is PolyLineSegment polylineSegment ? polylineSegment.Points : new PointCollection();

                                for (int i = 0; i < points.Count; i++)
                                {
                                    path.LineTo(
                                        density * (float)points[i].X,
                                        density * (float)points[i].Y);
                                }
                                lastPoint = points[^1];
                            }
                            // BezierSegment
                            else if (pathSegment is BezierSegment)
                            {
                                if (pathSegment is BezierSegment bezierSegment)
                                {
                                    path.CubicTo(
                                        density * (float)bezierSegment.Point1.X, density * (float)bezierSegment.Point1.Y,
                                        density * (float)bezierSegment.Point2.X, density * (float)bezierSegment.Point2.Y,
                                        density * (float)bezierSegment.Point3.X, density * (float)bezierSegment.Point3.Y);

                                    lastPoint = bezierSegment.Point3;
                                }
                            }
                            // PolyBezierSegment
                            else if (pathSegment is PolyBezierSegment)
                            {
                                PointCollection points = pathSegment is PolyBezierSegment polyBezierSegment ? polyBezierSegment.Points : new PointCollection();

                                if (points.Count >= 3)
                                {
                                    for (int i = 0; i < points.Count; i += 3)
                                    {
                                        path.CubicTo(
                                            density * (float)points[i + 0].X, density * (float)points[i + 0].Y,
                                            density * (float)points[i + 1].X, density * (float)points[i + 1].Y,
                                            density * (float)points[i + 2].X, density * (float)points[i + 2].Y);
                                    }
                                }

                                lastPoint = points[^1];
                            }
                            // QuadraticBezierSegment
                            else if (pathSegment is QuadraticBezierSegment)
                            {
                                if (pathSegment is QuadraticBezierSegment bezierSegment)
                                {
                                    path.QuadTo(
                                        density * (float)bezierSegment.Point1.X, density * (float)bezierSegment.Point1.Y,
                                        density * (float)bezierSegment.Point2.X, density * (float)bezierSegment.Point2.Y);

                                    lastPoint = bezierSegment.Point2;
                                }
                            }
                            // PolyQuadraticBezierSegment
                            else if (pathSegment is PolyQuadraticBezierSegment)
                            {
                                PointCollection points = pathSegment is PolyQuadraticBezierSegment polyBezierSegment ? polyBezierSegment.Points : new PointCollection();

                                if (points.Count >= 2)
                                {
                                    for (int i = 0; i < points.Count; i += 2)
                                    {
                                        path.QuadTo(
                                            density * (float)points[i + 0].X, density * (float)points[i + 0].Y,
                                            density * (float)points[i + 1].X, density * (float)points[i + 1].Y);
                                    }
                                }

                                lastPoint = points[^1];
                            }
                            // ArcSegment
                            else if (pathSegment is ArcSegment)
                            {
                                List<Point> points = new List<Point>();

                                if (pathSegment is ArcSegment arcSegment)
                                {
                                    GeometryHelper.FlattenArc(
                                        points,
                                        lastPoint,
                                        arcSegment.Point,
                                        arcSegment.Size.Width,
                                        arcSegment.Size.Height,
                                        arcSegment.RotationAngle,
                                        arcSegment.IsLargeArc,
                                        arcSegment.SweepDirection == SweepDirection.CounterClockwise,
                                        1);
                                }

                                for (int i = 0; i < points.Count; i++)
                                {
                                    path.LineTo(
                                        density * (float)points[i].X,
                                        density * (float)points[i].Y);
                                }

                                if (points.Count > 0)
                                    lastPoint = points[^1];
                            }
                        }

                        if (pathFigure.IsClosed)
                            path.Close();
                    }
            }

            return path;
        }
    }
}