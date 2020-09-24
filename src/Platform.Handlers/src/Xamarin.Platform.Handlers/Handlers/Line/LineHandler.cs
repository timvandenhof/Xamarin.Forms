namespace Xamarin.Platform.Handlers
{
    public partial class LineHandler
    {
		public static PropertyMapper<ILine> LineMapper = new PropertyMapper<ILine>(ShapeHandler.ShapeMapper)
		{
			[nameof(ILine.X1)] = MapPropertyX1,
			[nameof(ILine.Y1)] = MapPropertyY1,
			[nameof(ILine.X2)] = MapPropertyX2,
			[nameof(ILine.Y2)] = MapPropertyY2,
		};

		public LineHandler() : base(LineMapper)
		{

		}

		public LineHandler(PropertyMapper mapper) : base(mapper ?? LineMapper)
		{

		}
	}
}