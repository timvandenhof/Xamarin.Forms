using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Platform;

namespace Xamarin.Forms
{
	public partial class VisualElement : IView, IPropertyMapperView
	{

		SizeRequest _desiredSize;
		bool _isMeasureValid;
		bool _isArrangeValid;

		#region IView

		Rectangle IFrameworkElement.Frame => Bounds;

		protected IViewHandler Handler { get; set; }

		IViewHandler IFrameworkElement.Handler
		{
			get
			{
				return Handler;
			}

			set
			{
				Handler = value;
			}
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			(Handler)?.UpdateValue(propertyName);
		}

		IFrameworkElement IFrameworkElement.Parent => Parent as IView;

		SizeRequest IFrameworkElement.DesiredSize => _desiredSize;

		bool IFrameworkElement.IsMeasureValid => _isMeasureValid;

		bool IFrameworkElement.IsArrangeValid => _isArrangeValid;


		void IFrameworkElement.Arrange(Rectangle bounds)
		{
			if (_isArrangeValid)
				return;
			_isArrangeValid = true;
			Layout(bounds);
		}

		SizeRequest IFrameworkElement.Measure(double widthConstraint, double heightConstraint)
		{
			if (!_isMeasureValid)
				_desiredSize = this.Handler.GetDesiredSize(widthConstraint, heightConstraint);// this.OnMeasure(widthConstraint, heightConstraint);
			_isMeasureValid = true;
			return _desiredSize;
		}

		void IFrameworkElement.InvalidateMeasure()
		{
			_isMeasureValid = false;
			_isArrangeValid = false;
			this.InvalidateMeasure();
		}

		void IFrameworkElement.InvalidateArrange()
		{
			_isArrangeValid = false;
		}

		protected PropertyMapper propertyMapper;

		protected PropertyMapper<T> GetRendererOverides<T>() where T : IView => (PropertyMapper<T>)(propertyMapper as PropertyMapper<T> ?? (propertyMapper = new PropertyMapper<T>()));
		PropertyMapper IPropertyMapperView.GetPropertyMapperOverrides() => propertyMapper;


		#endregion
	}
}
