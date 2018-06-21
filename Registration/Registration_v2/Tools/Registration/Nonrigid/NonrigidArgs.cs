namespace Registration_v2.Tools.Registration.Nonrigid
{
    using Registration_v2.Data;

    /// <summary>
    /// An instance of the <see cref="NonrigidArgs"/> struct represents arguments necessary during a nonrigid registration.
    /// </summary>
    public struct NonrigidArgs
    {
        /// <summary>
        /// The source model used in a nonrigid registration.
        /// </summary>
        public Model3DFile SourceModel;

        /// <summary>
        /// The target model used in a nonrigid registration.
        /// </summary>
        public Model3DFile TargetModel;

        /// <summary>
        /// The number of iterations of rigid registration which is used during nonrigid registration.
        /// </summary>
        public int NumberOfIterations;

        /// <summary>
        /// Initializes a new instance of the <see cref="NonrigidArgs"/> struct.
        /// </summary>
        /// <param name="sourceModel">The source model used in a nonrigid registration.</param>
        /// <param name="targetModel">The target model used in a nonrigid registration.</param>
        /// <param name="numberOfIterations">The number of iterations of rigid registration which is used during nonrigid registration.</param>
        public NonrigidArgs(Model3DFile sourceModel, Model3DFile targetModel, int numberOfIterations)
        {
            this.SourceModel = sourceModel;
            this.TargetModel = targetModel;
            this.NumberOfIterations = numberOfIterations;
        }
    }
}
