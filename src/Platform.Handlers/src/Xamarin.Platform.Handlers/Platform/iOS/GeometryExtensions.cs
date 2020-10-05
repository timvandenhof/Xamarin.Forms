using System;
using System.Collections.Generic;
using CoreGraphics;
using Xamarin.Forms;

namespace Xamarin.Platform
{
    public static class GeometryExtensions
    {
        public static PathData ToNative(this Geometry geometry, Transform? renderTransform = null)
        {
            PathData pathData = new PathData
            {
                Data = new CGPath()
            };

            CGAffineTransform transform;

            if (renderTransform == null)
                transform = CGAffineTransform.MakeIdentity();
            else
                transform = renderTransform.ToNative();

            if (geometry is LineGeometry)
            {
				if (geometry is LineGeometry lineGeometry)
				{
					pathData.Data.MoveToPoint(transform, lineGeometry.StartPoint.ToPointF());
					pathData.Data.AddLineToPoint(transform, lineGeometry.EndPoint.ToPointF());
				}
			}
            else if (geometry is RectangleGeometry)
            {
                Rect? rect = (geometry as RectangleGeometry)?.Rect;

                if (rect != null && rect.HasValue)
                    pathData.Data.AddRect(transform, new CGRect(rect.Value.X, rect.Value.Y, rect.Value.Width, rect.Value.Height));
            }
            else if (geometry is EllipseGeometry)
            {
                if (geometry is EllipseGeometry ellipseGeometry)
                {
                    CGRect rect = new CGRect(
                        ellipseGeometry.Center.X - ellipseGeometry.RadiusX,
                        ellipseGeometry.Center.Y - ellipseGeometry.RadiusY,
                        ellipseGeometry.RadiusX * 2,
                        ellipseGeometry.RadiusY * 2);

                    pathData.Data.AddEllipseInRect(transform, rect);
                }
            }
            else if (geometry is GeometryGroup)
            {
                GeometryGroup? geometryGroup = geometry as GeometryGroup;

                pathData.IsNonzeroFillRule = geometryGroup?.FillRule == FillRule.Nonzero;

                if (geometryGroup != null)
                    foreach (Geometry child in geometryGroup.Children)
                    {
                        PathData pathChild = child.ToNative(renderTransform);
                        pathData.Data.AddPath(pathChild.Data);
                    }
            }
            else if (geometry is PathGeometry)
            {
                PathGeometry? pathGeometry = geometry as PathGeometry;

                pathData.IsNonzeroFillRule = pathGeometry?.FillRule == FillRule.Nonzero;

                if (pathGeometry != null)
                    foreach (PathFigure pathFigure in pathGeometry.Figures)
                    {
                        pathData.Data.MoveToPoint(transform, pathFigure.StartPoint.ToPointF());
                        Point lastPoint = pathFigure.StartPoint;

                        foreach (PathSegment pathSegment in pathFigure.Segments)
                        {
                            // LineSegment
                            if (pathSegment is LineSegment)
                            {
                                LineSegment? lineSegment = pathSegment as LineSegment;

                                if (lineSegment != null)
                                {
                                    pathData.Data.AddLineToPoint(transform, lineSegment.Point.ToPointF());
                                    lastPoint = lineSegment.Point;
                                }
                            }
                            // PolyLineSegment
                            else if (pathSegment is PolyLineSegment)
                            {
                                PolyLineSegment? polylineSegment = pathSegment as PolyLineSegment;
                                PointCollection points = polylineSegment != null ? polylineSegment.Points : new PointCollection();

                                for (int i = 0; i < points.Count; i++)
                                    pathData.Data.AddLineToPoint(transform, points[i].ToPointF());

                                lastPoint = points[points.Count - 1];
                            }

                            // BezierSegment
                            if (pathSegment is BezierSegment)
                            {
                                BezierSegment? bezierSegment = pathSegment as BezierSegment;

                                if (bezierSegment != null)
                                {
                                    pathData.Data.AddCurveToPoint(
                                        transform,
                                        bezierSegment.Point1.ToPointF(),
                                        bezierSegment.Point2.ToPointF(),
                                        bezierSegment.Point3.ToPointF());

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
                                        pathData.Data.AddCurveToPoint(
                                            transform,
                                            points[i].ToPointF(),
                                            points[i + 1].ToPointF(),
                                            points[i + 2].ToPointF());
                                    }
                                }

                                lastPoint = points[points.Count - 1];
                            }

                            // QuadraticBezierSegment
                            if (pathSegment is QuadraticBezierSegment)
                            {
                                QuadraticBezierSegment? bezierSegment = pathSegment as QuadraticBezierSegment;

                                if (bezierSegment != null)
                                {
                                    pathData.Data.AddQuadCurveToPoint(
                                        transform,
                                        new nfloat(bezierSegment.Point1.X),
                                        new nfloat(bezierSegment.Point1.Y),
                                        new nfloat(bezierSegment.Point2.X),
                                        new nfloat(bezierSegment.Point2.Y));

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
                                        pathData.Data.AddQuadCurveToPoint(
                                            transform,
                                            new nfloat(points[i + 0].X),
                                            new nfloat(points[i + 0].Y),
                                            new nfloat(points[i + 1].X),
                                            new nfloat(points[i + 1].Y));
                                    }
                                }

                                lastPoint = points[points.Count - 1];
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

                                CGPoint[] cgpoints = new CGPoint[points.Count];

                                for (int i = 0; i < points.Count; i++)
                                    cgpoints[i] = transform.TransformPoint(points[i].ToPointF());

                                pathData.Data.AddLines(cgpoints);

                                lastPoint = points.Count > 0 ? points[points.Count - 1] : Point.Zero;
                            }
                        }

                        if (pathFigure.IsClosed)
                            pathData.Data.CloseSubpath();
                    }
            }

            return pathData;
        }
    }
}