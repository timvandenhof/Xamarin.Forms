using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UIKit;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public static partial class FontExtensions
	{
		static readonly string DefaultFontName = UIFont.SystemFontOfSize(12).Name;

		public static UIFont ToUIFont(this Font self) => ToNativeFont(self);

		internal static UIFont ToUIFont(this IFont element) => ToNativeFont(element);

		static UIFont _ToNativeFont(string family, float size, FontAttributes attributes)
		{
			var bold = (attributes & FontAttributes.Bold) != 0;
			var italic = (attributes & FontAttributes.Italic) != 0;

			if (family != null && family != DefaultFontName)
			{
				try
				{
					UIFont? result = null;
					if (UIFont.FamilyNames.Contains(family))
					{
						var descriptor = new UIFontDescriptor().CreateWithFamily(family);

						if (bold || italic)
						{
							var traits = (UIFontDescriptorSymbolicTraits)0;
							if (bold)
								traits |= UIFontDescriptorSymbolicTraits.Bold;
							if (italic)
								traits |= UIFontDescriptorSymbolicTraits.Italic;

							descriptor = descriptor.CreateWithTraits(traits);
							result = UIFont.FromDescriptor(descriptor, size);
							if (result != null)
								return result;
						}
					}

					var cleansedFont = CleanseFontName(family);
					result = UIFont.FromName(cleansedFont, size);
					if (family.StartsWith(".SFUI", System.StringComparison.InvariantCultureIgnoreCase))
					{
						var fontWeight = family.Split('-').LastOrDefault();

						if (!string.IsNullOrWhiteSpace(fontWeight) && System.Enum.TryParse<UIFontWeight>(fontWeight, true, out var uIFontWeight))
						{
							result = UIFont.SystemFontOfSize(size, uIFontWeight);
							return result;
						}

						result = UIFont.SystemFontOfSize(size, UIFontWeight.Regular);
						return result;
					}
					if (result == null)
						result = UIFont.FromName(family, size);
					if (result != null)
						return result;
				}
				catch
				{
					Debug.WriteLine("Could not load font named: {0}", family);
				}
			}

			if (bold && italic)
			{
				var defaultFont = UIFont.SystemFontOfSize(size);

				var descriptor = defaultFont.FontDescriptor.CreateWithTraits(UIFontDescriptorSymbolicTraits.Bold | UIFontDescriptorSymbolicTraits.Italic);
				return UIFont.FromDescriptor(descriptor, 0);
			}

			if (italic)
				return UIFont.ItalicSystemFontOfSize(size);

			if (bold)
				return UIFont.BoldSystemFontOfSize(size);

			return UIFont.SystemFontOfSize(size);
		}

		internal static string? CleanseFontName(string fontName)
		{
			var fontFile = FontFile.FromString(fontName);

			// TODO: Complete porting the code when we have FontRegistrar.

			return fontFile.PostScriptName;
		}
	}
}