using System;

namespace Xamarin.Forms
{
	public class SkewTransform : Transform
	{
		double _angleX = 0.0;
		double _angleY = 0.0;
		double _centerX = 0.0;
		double _centerY = 0.0;

		public SkewTransform()
		{

		}

		public SkewTransform(double angleX, double angleY)
		{
			AngleX = angleX;
			AngleY = angleY;
		}

		public SkewTransform(double angleX, double angleY, double centerX, double centerY)
		{
			AngleX = angleX;
			AngleY = angleY;
			CenterX = centerX;
			CenterY = centerY;
		}

		public double AngleX
		{
			get { return _angleX; }
			set
			{
				_angleX = value;
				UpdateValue();
			}
		}

		public double AngleY
		{
			get { return _angleY; }
			set
			{
				_angleY = value;
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
			double radiansX = Math.PI * AngleX / 180;
			double radiansY = Math.PI * AngleY / 180;
			double tanX = Math.Tan(radiansX);
			double tanY = Math.Tan(radiansY);

			Value = new Matrix(1, tanY, tanX, 1, -CenterY * tanX, -CenterX * tanY);
		}
	}
}