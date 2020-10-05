namespace Xamarin.Forms
{
	public sealed class CompositeTransform : Transform
	{
		double _centerX = 0.0;
		double _centerY = 0.0;
		double _scaleX = 1.0;
		double _scaleY = 1.0;
		double _skewX = 0.0;
		double _skewY = 0.0;
		double _rotation = 0.0;
		double _translateX = 0.0;
		double _translateY = 0.0;

		public double CenterX 
		{
			get { return _centerX; }
			set
			{
				_centerX = value;
				UpdateValue();
			}
		}

		public double CenterY
		{
			get { return _centerY; }
			set
			{
				_centerY = value;
				UpdateValue();
			}
		}

		public double ScaleX
		{
			get { return _scaleX; }
			set
			{
				_scaleX = value;
				UpdateValue();
			}
		}

		public double ScaleY
		{
			get { return _scaleY; }
			set
			{
				_scaleY = value;
				UpdateValue();
			}
		}

		public double SkewX
		{
			get { return _skewX; }
			set
			{
				_skewX = value;
				UpdateValue();
			}
		}

		public double SkewY
		{
			get { return _skewY; }
			set
			{
				_skewY = value;
				UpdateValue();
			}
		}

		public double Rotation
		{
			get { return _rotation; }
			set
			{
				_rotation = value;
				UpdateValue();
			}
		}

		public double TranslateX
		{
			get { return _translateX; }
			set
			{
				_translateX = value;
				UpdateValue();
			}
		}

		public double TranslateY
		{
			get { return _translateY; }
			set
			{
				_translateY = value;
				UpdateValue();
			}
		}

		void UpdateValue()
		{
			TransformGroup xformGroup = new TransformGroup
			{
				Children =
				{
					new TranslateTransform
					{
						X = -CenterX,
						Y = -CenterY
					},
					new ScaleTransform
					{
						ScaleX = ScaleX,
						ScaleY = ScaleY
					},
					new SkewTransform
					{
						AngleX = SkewX,
						AngleY = SkewY
					},
					new RotateTransform
					{
						Angle = Rotation
					},
					new TranslateTransform
					{
						X = CenterX,
						Y = CenterY
					},
					new TranslateTransform
					{
						X = TranslateX,
						Y = TranslateY
					}
				}
			};

			Value = xformGroup.Value;
		}
	}
}