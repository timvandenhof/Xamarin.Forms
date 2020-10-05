namespace Xamarin.Platform.Handlers
{
    public partial class LineHandler 
	{
		public static PropertyMapper<ILine, LineHandler> LineMapper = new PropertyMapper<ILine, LineHandler>(ShapeHandler.ShapeMapper)
		{
			[nameof(ILine.X1)] = MapX1,
			[nameof(ILine.Y1)] = MapY1,
			[nameof(ILine.X2)] = MapX2,
			[nameof(ILine.Y2)] = MapY2
		};

		public static void MapX1(LineHandler handler, ILine line)
		{
			ViewHandler.CheckParameters(handler, line);
			handler.TypedNativeView?.UpdateX1(line);
		}

		public static void MapY1(LineHandler handler, ILine line)
		{
			ViewHandler.CheckParameters(handler, line);
			handler.TypedNativeView?.UpdateY1(line);
		}

		public static void MapX2(LineHandler handler, ILine line)
		{
			ViewHandler.CheckParameters(handler, line);
			handler.TypedNativeView?.UpdateX2(line);
		}

		public static void MapY2(LineHandler handler, ILine line)
		{
			ViewHandler.CheckParameters(handler, line);
			handler.TypedNativeView?.UpdateY2(line);
		}

		public LineHandler() : base(LineMapper)
		{

		}

		public LineHandler(PropertyMapper mapper) : base(mapper ?? LineMapper)
		{

		}
	}
}