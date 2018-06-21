namespace Registration_v2.Tools.Registration.Rigid
{
    using Registration_v2.Data;

    /// <summary>
    /// An instance of the <see cref="RigidArgs"/> struct represents arguments necessary during a rrigid registration.
    /// </summary>
    public struct RigidArgs
    {
        /// <summary>
        /// The source model used in a rigid registration.
        /// </summary>
        public Model3DFile SourceModel;

        /// <summary>
        /// The target model used in a rigid registration.
        /// </summary>
        public Model3DFile TargetModel;

        /// <summary>
        /// The point mapping algorithm used in a rigid registration.
        /// </summary>
        public IPointMapping MappingAlgorithm;

        /// <summary>
        /// The number of iterations.
        /// </summary>
        public int NumberOfIterations;

        /// <summary>
        /// Initializes a new instance of the <see cref="RigidArgs"/> struct.
        /// </summary>
        /// <param name="sourceModel"> The source model used in a rigid registration.</param>
        /// <param name="targetModel">The target model used in a rigid registration.</param>
        /// <param name="mappingAlgorithm">The point mapping algorithm used in a rigid registration.</param>
        /// <param name="numberOfIterations">The number of iterations.</param>
        public RigidArgs(Model3DFile sourceModel, Model3DFile targetModel, IPointMapping mappingAlgorithm, int numberOfIterations)
        {
            this.SourceModel = sourceModel;
            this.TargetModel = targetModel;
            this.MappingAlgorithm = mappingAlgorithm;
            this.NumberOfIterations = numberOfIterations;
        }
    }
}
