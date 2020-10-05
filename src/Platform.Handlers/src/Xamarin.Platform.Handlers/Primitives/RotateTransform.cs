using System;

namespace Xamarin.Forms
{
	public class RotateTransform : Transform
	{
		double _angle = 0.0;
		double _centerX = 0.0;
		double _centerY = 0.0;

		public RotateTransform()
		{

		}

		public RotateTransform(double angle)
		{
			Angle = angle;
		}

		public RotateTransform(double angle, double centerX, double centerY)
		{
			Angle = angle;
			CenterX = centerX;
			CenterY = centerY;
		}

		public double Angle
		{
			get { return _angle; }
			set
			{
				_angle = value;
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
			double radians = Math.PI * Angle / 180;
			double sin = Math.Sin(radians);
			double cos = Math.Cos(radians);

			Value = new Matrix(cos, sin, -sin, cos, CenterX * (1 - cos) + CenterY * sin, CenterY * (1 - cos) - CenterX * sin);
		}
	}
}