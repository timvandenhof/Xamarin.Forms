using System.Collections.Generic;
using Xamarin.Forms;
using System;

#if __MOBILE__
using UIKit;
using NativeFont = UIKit.UIFont;
#else
using AppKit;
using NativeFont = AppKit.NSFont;
# endif

namespace Xamarin.Platform
{
	public static partial class FontExtensions
	{
		static readonly Dictionary<ToNativeFontFontKey, NativeFont> ToUiFont = new Dictionary<ToNativeFontFontKey, NativeFont>();

		static NativeFont ToNativeFont(this IFont element)
		{
			var fontFamily = element.FontFamily;
			var fontSize = (float)element.FontSize;
			var fontAttributes = element.FontAttributes;
			return ToNativeFont(fontFamily, fontSize, fontAttributes, _ToNativeFont);
		}

		static NativeFont ToNativeFont(this Font self)
		{
			var size = (float)self.FontSize;
			if (self.UseNamedSize)
			{
				switch (self.NamedSize)
				{
					case NamedSize.Micro:
						size = 12;
						break;
					case NamedSize.Small:
						size = 14;
						break;
					case NamedSize.Medium:
						size = 17; // as defined by iOS documentation
						break;
					case NamedSize.Large:
						size = 22;
						break;
					default:
						size = 17;
						break;
				}
			}

			var fontAttributes = self.FontAttributes;

			return ToNativeFont(self.FontFamily, size, fontAttributes, _ToNativeFont);
		}

		static NativeFont ToNativeFont(string family, float size, FontAttributes attributes, Func<string, float, FontAttributes, NativeFont> factory)
		{
			var key = new ToNativeFontFontKey(family, size, attributes);

			lock (ToUiFont)
			{
				if (ToUiFont.TryGetValue(key, out NativeFont value))
					return value;
			}

			var generatedValue = factory(family, size, attributes);

			lock (ToUiFont)
			{
				if (!ToUiFont.TryGetValue(key, out NativeFont value))
					ToUiFont.Add(key, value = generatedValue);
				return value;
			}
		}

		struct ToNativeFontFontKey
		{
			internal ToNativeFontFontKey(string family, float size, FontAttributes attributes)
			{
				_family = family;
				_size = size;
				_attributes = attributes;
			}
#pragma warning disable 0414 // these are not called explicitly, but they are used to establish uniqueness. allow it!
			string _family;
			float _size;
			FontAttributes _attributes;
#pragma warning restore 0414
		}
	}
}