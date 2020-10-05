namespace Xamarin.Forms
{
	public class MatrixTransform : Transform
    {
        Matrix _matrix;

        public MatrixTransform()
        {

        }

        public MatrixTransform(Matrix matrix)
        {
            Matrix = matrix;
        }

        public Matrix Matrix
        {
            get { return _matrix; }
            set
            {
                _matrix = value;
                UpdateValue();
            }
        }

        void UpdateValue()
        {
            Value = Matrix;
        }
    }
}
