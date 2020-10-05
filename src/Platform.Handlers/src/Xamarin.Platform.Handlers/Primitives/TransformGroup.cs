using System.Collections.Specialized;
using System.ComponentModel;

namespace Xamarin.Forms
{
	public sealed class TransformGroup : Transform
	{
		public TransformGroup()
		{
			Children = new TransformCollection();

			Children.CollectionChanged += OnChildrenCollectionChanged;
		}

		public TransformCollection Children { get; set; }

		void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			if (args.NewItems != null)
				foreach (INotifyPropertyChanged item in args.NewItems)
				{
					item.PropertyChanged += OnTransformPropertyChanged;
				}

			if (args.OldItems != null)
				foreach (INotifyPropertyChanged item in args.OldItems)
				{
					item.PropertyChanged -= OnTransformPropertyChanged;
				}

			UpdateTransformMatrix();
		}

		void OnTransformPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			UpdateTransformMatrix();
		}

		void UpdateTransformMatrix()
		{
			var matrix = new Matrix();

			foreach (Transform child in Children)
				matrix = Matrix.Multiply(matrix, child.Value);

			Value = matrix;
		}
	}
}