namespace Registration.Rigid
{
    using System;

    public class Vector3 : IVector
    {
        private const int LENGTH = 3;

        private float[] values = new float[LENGTH];

        public Vector3(float x, float y, float z)
        {
            this.values[0] = x;
            this.values[1] = y;
            this.values[2] = z;
        }

        public float X => this.values[0];

        public float Y => this.values[1];

        public float Z => this.values[2];

        public int Size => LENGTH;


        public float GetValue(int index)
        {
            return this.values[index];
        }

        public float Distance(IVector vector)
        {
            if (this.GetType() != typeof(Vector3))
            {
                throw new ArgumentException("Argument has to be type of the Vector3 class.");
            }

            float sum = 0;
            for (int i = 0; i < LENGTH; i++)
            {
                sum += (this.values[i] - vector.GetValue(i)) * (this.values[i] - vector.GetValue(i));
            }

            return (float)Math.Sqrt(sum);
        }
    }
}
