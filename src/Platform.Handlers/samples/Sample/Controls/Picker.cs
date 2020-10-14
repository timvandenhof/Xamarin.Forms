using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
	public class Picker : View, IPicker
	{
		public Picker()
		{

		}

		public string Title { get; set; }

		public Color TitleColor { get; set; }

		public IList<string> Items { get; } = new LockableObservableListWrapper();

		public IList ItemsSource { get; set; }

		public int SelectedIndex { get; set; } = -1;

		public object SelectedItem { get; set; }

		public string Text { get; set; }

		public Color Color { get; set; }

		public Font Font { get; set; }

		public TextTransform TextTransform { get; set; }

		public double CharacterSpacing { get; set; }

		public FontAttributes FontAttributes { get; set; }

		public string FontFamily { get; set; }

		public double FontSize => throw new System.NotImplementedException();

		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }

		public Action SelectedIndexChanged { get; set; }

		void IPicker.SelectedIndexChanged() => SelectedIndexChanged?.Invoke();
	}
}