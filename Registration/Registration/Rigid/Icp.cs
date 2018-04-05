namespace Registration.Rigid
{
    class Icp
    {
        private IRotation rotation;

        public Icp(IRotation rotationAlgorithm)
        {
            this.rotation = rotationAlgorithm;
        }

        public void ComputeTrasformation(float maxError)
        {

        }

        public void ComputeTransformation(int iterationCount)
        {

        }
    }
}
