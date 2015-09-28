using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace Lab1
{
    class Program
    {
        static void Main()
        {
            var averageInfoAmount = new double[] { 100, 0, 200, 0, 100, 0, 0, 200, 300, 0 };
            var operationsPerOperator = new double[] { 0, 50, 0, 75, 0, 100, 75, 0, 0, 50 };
            var numberFile = new int[] { 1, 0, 2, 0, 1, 0, 0, 2, 3, 0 };

            var probabilityMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {0, 1, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, (2 / 7.0) * (1 / 6.0), (2 / 7.0) * (5 / 6.0), 0, (5 / 7.0) * (4 / 5.0), (5 / 7.0) * (1 / 5.0), 0, 0, 0},
                {0, 0, 0, 0, 0, 4 / 5.0, 1 / 5.0, 0, 0, 0},
                {0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                {0, 0, 1 / 6.0, 5 / 6.0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 1 / 5.0, 4 / 5.0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 1 / 5.0, 4 / 5.0},
            });

            var constantTermsVector = Vector<double>.Build.Dense(new double[] { -1, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            // Linear Equation System solving 
            probabilityMatrix = probabilityMatrix.Transpose();
            for (var i = 0; i < probabilityMatrix.RowCount; i++)
            {
                probabilityMatrix[i, i] = probabilityMatrix[i, i] - 1;
            }
            var solutions = probabilityMatrix.Solve(constantTermsVector).ToArray();

            // Q
            var averageOperationsPerRun = averageInfoAmount.Select(((ki, i) => ki * solutions[i])).Sum();

            // N1, N2, N3
            double[] averageFileAccesses = { 0, 0, 0 };

            for (var i = 0; i < numberFile.Length; i++)
            {
                switch (numberFile[i])
                {
                    case 1:
                        averageFileAccesses[0] += solutions[i];
                        break;
                    case 2:
                        averageFileAccesses[1] += solutions[i];
                        break;
                    case 3:
                        averageFileAccesses[2] += solutions[i];
                        break;
                }
            }

            // Q1, Q2, Q3
            double[] averageInfoPutToFilePerRun = { 0, 0, 0 };
            double averageOperatorAccesses = 0;
            for (var i = 0; i < numberFile.Length; i++)
            {
                switch (numberFile[i])
                {
                    case 1:
                        averageInfoPutToFilePerRun[0] += solutions[i] * averageInfoAmount[i];
                        break;
                    case 2:
                        averageInfoPutToFilePerRun[1] += solutions[i] * averageInfoAmount[i];
                        break;
                    case 3:
                        averageInfoPutToFilePerRun[2] += solutions[i] * averageInfoAmount[i];
                        break;
                    default:
                        averageOperatorAccesses += solutions[i];
                        break;
                }
            }

            for (var i = 0; i < averageInfoPutToFilePerRun.Length; i++)
                averageInfoPutToFilePerRun[i] = averageInfoPutToFilePerRun[i] * (1 / averageFileAccesses[i]);

            // Q0
            var averageOperatorOperations = averageOperationsPerRun / averageOperatorAccesses;

            for (var i = 0; i < solutions.Length; i++)
            {
                Console.WriteLine("n[" + (i + 1) + "] = " + solutions[i]);
            }

            Console.WriteLine(Environment.NewLine + "Q = " + averageOperationsPerRun);

            for (var i = 0; i < averageFileAccesses.Length; i++)
            {
                Console.WriteLine("N" + (i + 1) + " = " + averageFileAccesses[i] + ", Q" + (i + 1) + " = " + averageInfoPutToFilePerRun[i]);
            }

            Console.WriteLine(Environment.NewLine + "Q0 = " + averageOperatorOperations);
            Console.Read();
        }
    }
}
