using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Platform;
using Xamarin.Platform.Layouts;

namespace Xamarin.Forms
{
	public partial class VisualElement : IView, IPropertyMapperView
	{



		#region IView

		Rectangle IFrameworkElement.Frame => Bounds;

		public IViewHandler Handler { get; set; }

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			(Handler)?.UpdateValue(propertyName);
		}

		IFrameworkElement IFrameworkElement.Parent => Parent as IView;

		public Size DesiredSize { get; protected set; }

		public bool IsMeasureValid { get; protected set; }

		public bool IsArrangeValid { get; protected set; }

		public void Arrange(Rectangle bounds)
		{
			if (IsArrangeValid)
				return;
			IsArrangeValid = true;
			Layout(bounds);
		}

		Size IFrameworkElement.Measure(double widthConstraint, double heightConstraint)
		{
			if (!IsMeasureValid)
			{
				// TODO ezhart Adjust constraints to account for margins

				// TODO ezhart If we can find reason to, we may need to add a MeasureFlags parameter to IFrameworkElement.Measure
				// Forms has and (very occasionally) uses one. I'd rather not muddle this up with it, but if it's necessary
				// we can add it. The default is MeasureFlags.None, but nearly every use of it is MeasureFlags.IncludeMargins,
				// so it's an awkward default. 

				// I'd much rather just get rid of all the uses of it which don't include the margins, and have "with margins"
				// be the default. It's more intuitive and less code to write. Also, I sort of suspect that the uses which
				// _don't_ include the margins are actually bugs.

				var frameworkElement = this as IFrameworkElement;

				widthConstraint = LayoutManager.ResolveConstraints(widthConstraint, frameworkElement.Width);
				heightConstraint = LayoutManager.ResolveConstraints(heightConstraint, frameworkElement.Height);

				DesiredSize = Handler.GetDesiredSize(widthConstraint, heightConstraint);
			}

			IsMeasureValid = true;
			return DesiredSize;
		}

		void IFrameworkElement.InvalidateMeasure()
		{
			IsMeasureValid = false;
			IsArrangeValid = false;
			InvalidateMeasure();
		}

		void IFrameworkElement.InvalidateArrange()
		{
			IsArrangeValid = false;
		}

		protected PropertyMapper propertyMapper;

		protected PropertyMapper<T> GetRendererOverides<T>() where T : IView => (PropertyMapper<T>)(propertyMapper as PropertyMapper<T> ?? (propertyMapper = new PropertyMapper<T>()));
		PropertyMapper IPropertyMapperView.GetPropertyMapperOverrides() => propertyMapper;

		double IFrameworkElement.Width { get => WidthRequest; }
		double IFrameworkElement.Height { get => HeightRequest; }

		#endregion
	}
}
