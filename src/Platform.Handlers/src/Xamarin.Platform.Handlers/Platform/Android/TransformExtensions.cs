﻿using Xamarin.Forms;
using AMatrix = Android.Graphics.Matrix;

namespace Xamarin.Platform
{
	public static class TransformExtensions
	{
		public static AMatrix ToNative(this Transform? transform, float density = 1)
		{
			AMatrix aMatrix = new AMatrix();

			if (transform == null)
				return aMatrix;

			Matrix matrix = transform.Value;

			aMatrix.SetValues(
				new float[] {
					(float)matrix.M11,
					(float)matrix.M21,
					(float)matrix.OffsetX * density,
					(float)matrix.M12,
					(float)matrix.M22,
					(float)matrix.OffsetY * density,
					0,
					0,
					1 });

			return aMatrix;
		}
	}
}