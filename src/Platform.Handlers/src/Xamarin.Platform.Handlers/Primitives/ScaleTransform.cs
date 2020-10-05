namespace Xamarin.Forms
{
	public class ScaleTransform : Transform
	{
		double _scaleX = 1.0;
		double _scaleY = 1.0;
		double _centerX = 0.0;
		double _centerY = 0.0;

		public ScaleTransform()
		{

		}

		public ScaleTransform(double scaleX, double scaleY)
		{
			ScaleX = scaleX;
			ScaleY = scaleY;
		}

		public ScaleTransform(double scaleX, double scaleY, double centerX, double centerY)
		{
			ScaleX = scaleX;
			ScaleY = scaleY;
			CenterX = centerX;
			CenterY = centerY;
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

		void UpdateValue()
		{
			Value = new Matrix(ScaleX, 0, 0, ScaleY, CenterX * (1 - ScaleX), CenterY * (1 - ScaleY));
		}
	}
}