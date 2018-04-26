namespace Registration.Nonrigid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    class ControlPointsGenerator
    {
        public const int AREA_SIZE = 3;

        public static List<Vector<float>> RandomPoints(List<Vector<float>> sourcePoints)
        {
            List<Vector<float>> controlPoints = new List<Vector<float>>();
            int controlPointsCount = sourcePoints.Count / 10;
            int index = 0;

            Random random = new Random();

            while (index < controlPointsCount)
            {
                Vector<float> tempPoint = sourcePoints[random.Next(sourcePoints.Count)];
                if (!controlPoints.Contains(tempPoint))
                {
                    bool isOk = true;
                    for (int i = 0; i < controlPoints.Count; i++)
                    {
                        if (Distance.Euclidean(controlPoints[i], tempPoint) < AREA_SIZE)
                        {
                            isOk = false;
                            break;
                        }
                    }

                    if (isOk)
                    {
                        controlPoints.Add(tempPoint);
                        index++;
                    }
                }
            }

            return controlPoints;
        }
    }
}
