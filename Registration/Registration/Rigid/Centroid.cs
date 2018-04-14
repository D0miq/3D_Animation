namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// 
    /// </summary>
    public class Centroid
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Centroid(List<Vector<float>> points)
        {
            this.Value = Vector<float>.Build.Dense(points[0].Count);

            for (int i = 0; i < points.Count; i++)
            {
                this.Value += points[i];
            }

            this.Value = this.Value.Divide(points.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector<float> Value { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Vector<float>> TranslatePointsToOrigin(List<Vector<float>> points)
        {
            Vector<float>[] tempPoints = new Vector<float>[points.Count];

            for (int i = 0; i < points.Count; i++)
            {
                tempPoints[i] = points[i] - this.Value;
            }

            return new List<Vector<float>>(tempPoints);
        }
    }
}
