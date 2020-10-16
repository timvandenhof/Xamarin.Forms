using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
	public class FontElement : View, IFont
	{
		FontAttributes _fontAttributes;
		string _fontFamily;
		double _fontSize;

		public Font Font { get; set; }

		public FontAttributes FontAttributes
		{
			get { return _fontAttributes; }
			set
			{
				_fontAttributes = value;
				UpdateFont();
			}
		}

		public string FontFamily
		{
			get { return _fontFamily; }
			set
			{
				_fontFamily = value;
				UpdateFont();
			}
		}

		public double FontSize
		{
			get { return _fontSize; }
			set
			{
				_fontSize = value;
				UpdateFont();
			}
		}

		void UpdateFont()
		{
			if (_fontFamily != null)
				Font = Font.OfSize(_fontFamily, _fontSize).WithAttributes(_fontAttributes);
			else
				Font = Font.SystemFontOfSize(_fontSize, _fontAttributes);
		}
	}
}