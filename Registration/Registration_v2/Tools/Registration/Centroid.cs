namespace Registration_v2.Tools.Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// An instance of the <see cref="Centroid"/> class represents a centroid of a list of points.
    /// </summary>
    public class Centroid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Centroid"/> class and computes a centroid from the given points.
        /// </summary>
        /// <param name="points">The points.</param>
        public Centroid(List<Vector<float>> points)
        {
            this.Value = Vector<float>.Build.Dense(4);

            for (int i = 0; i < points.Count; i++)
            {
                this.Value += points[i];
            }

            this.Value = this.Value.Divide(points.Count);
        }

        /// <summary>
        /// Gets the centroid vector.
        /// </summary>
        public Vector<float> Value { get; }
    }
}
