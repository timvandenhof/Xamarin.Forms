namespace Xamarin.Platform.Handlers
{
    public partial class ShapeHandler
    {
		public static PropertyMapper<IShape> ShapeMapper = new PropertyMapper<IShape>(ViewHandler.ViewMapper)
		{
			[nameof(IShape.Frame)] = MapPropertyFrame,
			[nameof(IShape.Fill)] = MapPropertyFill,
			[nameof(IShape.Stroke)] = MapPropertyStroke,
			[nameof(IShape.StrokeThickness)] = MapPropertyStrokeThickness,
			[nameof(IShape.StrokeDashArray)] = MapPropertyStrokeDashArray,
			[nameof(IShape.StrokeDashOffset)] = MapPropertyStrokeDashOffset,
			[nameof(IShape.StrokeLineCap)] = MapPropertyStrokeLineCap,
			[nameof(IShape.StrokeLineJoin)] = MapPropertyStrokeLineJoin,
			[nameof(IShape.StrokeMiterLimit)] = MapPropertyStrokeMiterLimit,
			[nameof(IShape.Aspect)] = MapPropertyAspect
		};
	}
}