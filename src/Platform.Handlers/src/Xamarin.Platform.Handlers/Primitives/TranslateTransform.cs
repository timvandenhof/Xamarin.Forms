namespace Xamarin.Forms
{
	public class TranslateTransform : Transform
	{
		double _x = 0.0;
		double _y = 0.0;

		public TranslateTransform()
		{

		}

		public TranslateTransform(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double X
		{
			get { return _x; }
			set
			{
				_x = value;
				UpdateValue();
			}
		}

		public double Y
		{
			get { return _y; }
			set
			{
				_y = value;
				UpdateValue();
			}
		}

		void UpdateValue()
		{
			Value = new Matrix(1, 0, 0, 1, X, Y);
		}
	}
}