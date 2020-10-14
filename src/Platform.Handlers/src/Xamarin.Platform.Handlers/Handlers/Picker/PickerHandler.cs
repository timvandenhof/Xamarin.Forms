namespace Xamarin.Platform.Handlers
{
	public partial class PickerHandler
	{
		public static PropertyMapper<IPicker, PickerHandler> PickerMapper = new PropertyMapper<IPicker, PickerHandler>(ViewHandler.ViewMapper)
		{
			[nameof(IPicker.Title)] = MapTitle,
			[nameof(IPicker.TitleColor)] = MapTitleColor,
			[nameof(IPicker.Color)] = MapTextColor,
			[nameof(IPicker.SelectedIndex)] = MapSelectedIndex,

			[nameof(IText.Font)] = MapFont,
			[nameof(IText.FontAttributes)] = MapFont,
			[nameof(IText.FontFamily)] = MapFont,
			[nameof(IText.FontSize)] = MapFont,
			[nameof(IText.CharacterSpacing)] = MapCharacterSpacing,
			[nameof(IText.HorizontalTextAlignment)] = MapHorizontalTextAlignment,
			[nameof(IText.VerticalTextAlignment)] = MapVerticalTextAlignment
		};

		public static void MapTitle(PickerHandler handler, IPicker picker)
		{
			ViewHandler.CheckParameters(handler, picker);
			handler.TypedNativeView?.UpdateTitle(picker);
		}

		public static void MapTitleColor(PickerHandler handler, IPicker picker)
		{
			ViewHandler.CheckParameters(handler, picker);
			handler.TypedNativeView?.UpdateTitleColor(picker);
		}

		public static void MapTextColor(PickerHandler handler, IPicker picker)
		{
			ViewHandler.CheckParameters(handler, picker);
			handler.TypedNativeView?.UpdateTextColor(picker);
		}

		public static void MapSelectedIndex(PickerHandler handler, IPicker picker)
		{
			ViewHandler.CheckParameters(handler, picker);
			handler.TypedNativeView?.UpdateSelectedIndex(picker);
		}

		public static void MapFont(PickerHandler handler, IPicker picker)
		{
			ViewHandler.CheckParameters(handler, picker);
			handler.TypedNativeView?.UpdateFont(picker);
		}

		public static void MapCharacterSpacing(PickerHandler handler, IPicker picker)
		{
			ViewHandler.CheckParameters(handler, picker);
			handler.TypedNativeView?.UpdateCharacterSpacing(picker);
		}

		public static void MapHorizontalTextAlignment(PickerHandler handler, IPicker picker)
		{
			ViewHandler.CheckParameters(handler, picker);
			handler.TypedNativeView?.UpdateHorizontalTextAlignment(picker);
		}

		public static void MapVerticalTextAlignment(PickerHandler handler, IPicker picker)
		{
			ViewHandler.CheckParameters(handler, picker);
			handler.TypedNativeView?.UpdateVerticalTextAlignment(picker);
		}

		public PickerHandler() : base(PickerMapper)
		{

		}

		public PickerHandler(PropertyMapper mapper) : base(mapper)
		{

		}
	}
}