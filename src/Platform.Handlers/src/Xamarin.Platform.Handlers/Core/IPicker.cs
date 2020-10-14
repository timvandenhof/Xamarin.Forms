using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public interface IPicker : IText
	{
		string Title { get; }
		Color TitleColor { get; }
		IList<string> Items { get; }
		IList ItemsSource { get; }
		int SelectedIndex { get; set; }
		object? SelectedItem { get; set; }

		void SelectedIndexChanged();
	}
}