using System;
using System.Collections.Concurrent;
using Xamarin.Forms;
using Android.Graphics;
using AApplication = Android.App.Application;

namespace Xamarin.Platform
{
	public static class FontExtensions
	{
		static Typeface? DefaultTypeface;

		static readonly ConcurrentDictionary<Tuple<string, FontAttributes>, Typeface?> Typefaces = new ConcurrentDictionary<Tuple<string, FontAttributes>, Typeface?>();

		public static Typeface? ToTypeface(this IFont self)
		{
			if (self.FontAttributes == FontAttributes.None && string.IsNullOrEmpty(self.FontFamily))
				return DefaultTypeface ??= Typeface.Default;

			return ToTypeface(self.FontFamily, self.FontAttributes);
		}

		public static Typeface? ToTypeface(this Font self)
		{
			if (self.IsDefault || (self.FontAttributes == FontAttributes.None && string.IsNullOrEmpty(self.FontFamily)))
				return DefaultTypeface ??= Typeface.Default;

			return ToTypeface(self.FontFamily, self.FontAttributes);
		}

		public static float ToScaledPixel(this Font self)
		{
			if (self.IsDefault)
				return 14;

			if (self.UseNamedSize)
			{
				switch (self.NamedSize)
				{
					case NamedSize.Micro:
						return 10;

					case NamedSize.Small:
						return 12;

					case NamedSize.Default:
					case NamedSize.Medium:
						return 14;

					case NamedSize.Large:
						return 18;
				}
			}

			return (float)self.FontSize;
		}

		public static TypefaceStyle ToTypefaceStyle(FontAttributes attrs)
		{
			var style = TypefaceStyle.Normal;

			if ((attrs & (FontAttributes.Bold | FontAttributes.Italic)) == (FontAttributes.Bold | FontAttributes.Italic))
				style = TypefaceStyle.BoldItalic;
			else if ((attrs & FontAttributes.Bold) != 0)
				style = TypefaceStyle.Bold;
			else if ((attrs & FontAttributes.Italic) != 0)
				style = TypefaceStyle.Italic;

			return style;
		}

		internal static Typeface? ToTypeFace(this string fontfamily, FontAttributes attr = FontAttributes.None)
		{
			fontfamily ??= string.Empty;
			var style = ToTypefaceStyle(attr);
			return Typeface.Create(fontfamily, style);
		}

		static Typeface? ToTypeface(string fontFamily, FontAttributes fontAttributes)
		{
			fontFamily ??= string.Empty;
			return Typefaces.GetOrAdd(new Tuple<string, FontAttributes>(fontFamily, fontAttributes), CreateTypeface);
		}

		static string FontNameToFontFile(string fontFamily)
		{
			fontFamily ??= string.Empty;
			int hashtagIndex = fontFamily.IndexOf('#');
			if (hashtagIndex >= 0)
				return fontFamily.Substring(0, hashtagIndex);

			throw new InvalidOperationException($"Can't parse the {nameof(fontFamily)} {fontFamily}");
		}

		static bool IsAssetFontFamily(string name)
		{
			return name != null && (name.Contains(".ttf#") || name.Contains(".otf#"));
		}

		static Typeface? CreateTypeface(Tuple<string, FontAttributes> key)
		{
			Typeface? result;
			var fontFamily = key.Item1;
			var fontAttribute = key.Item2;

			if (string.IsNullOrWhiteSpace(fontFamily))
			{
				var style = ToTypefaceStyle(fontAttribute);
				result = Typeface.Create(Typeface.Default, style);
			}
			else if (IsAssetFontFamily(fontFamily))
			{
				result = Typeface.CreateFromAsset(AApplication.Context.Assets, FontNameToFontFile(fontFamily));
			}
			else
			{
				result = fontFamily.ToTypeFace(fontAttribute);
			}

			return result;
		}
	}
}