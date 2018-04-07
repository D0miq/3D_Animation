namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;

    public class Centroid
    {
        private List<Vector<float>> points;

        public Centroid(List<Vector<float>> points)
        {
            this.Value = Vector<float>.Build.Dense(points[0].Count);
            this.points = points;
            for (int i = 0; i < points.Count; i++)
            {
                this.Value += points[i];
            }

            this.Value.Divide(points.Count);
        }

        public Vector<float> Value { get; }

        public List<Vector<float>> NormalizePoints()
        {
            List<Vector<float>> tempPoints = new List<Vector<float>>();

            for (int i = 0; i < this.points.Count; i++)
            {
                tempPoints[i] = this.points[i] - this.Value;
            }

            return tempPoints;
        }
    }
}
