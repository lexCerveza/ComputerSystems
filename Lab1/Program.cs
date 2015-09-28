using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace Lab1
{
    class Program
    {
        static void Main()
        {
            var k = new double[] { 100, 0, 200, 0, 100, 0, 0, 200, 300, 0 };
            var l = new double[] { 0, 50, 0, 75, 0, 100, 75, 0, 0, 50 };
            var numberFile = new [] { 1, 0, 2, 0, 1, 0, 0, 2, 3, 0 };

            var a = Matrix<double>.Build.DenseOfArray(new [,]
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

            var b = Vector<double>.Build.Dense(new double[] { -1, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            // Linear Equation System solving 
            a = a.Transpose();
            for (var i = 0; i < a.RowCount; i++)
            {
                a[i, i] = a[i, i] - 1;
            }
            var n = a.Solve(b).ToArray();

            // Q
            var q = k.Select(((ki, i) => ki * n[i])).Sum();

            // N1, N2, N3
            double n1 = 0;
            double n2 = 0;
            double n3 = 0;
            for (var i = 0; i < numberFile.Length; i++)
            {
                switch (numberFile[i])
                {
                    case 1:
                        n1 += n[i];
                        break;
                    case 2:
                        n2 += n[i];
                        break;
                    case 3:
                        n3 += n[i];
                        break;
                }
            }

            // Q1, Q2, Q3
            double q1 = 0;
            double q2 = 0;
            double q3 = 0;
            double N = 0;
            for (var i = 0; i < numberFile.Length; i++)
            {
                switch (numberFile[i])
                {
                    case 1:
                        q1 += n[i] * k[i];
                        break;
                    case 2:
                        q2 += n[i] * k[i];
                        break;
                    case 3:
                        q3 += n[i] * k[i];
                        break;
                    default:
                        N += n[i];
                        break;
                }
            }

            q1 = q1 * (1 / n1);
            q2 = q2 * (1 / n2);
            q3 = q3 * (1 / n3);

            // Q0
            var q0 = q / N;

            for (var i = 0; i < n.Length; i++)
            {
                Console.WriteLine("n[" + (i + 1) + "] = " + n[i]);
            }

            Console.WriteLine("\nQ = " + q);

            Console.WriteLine("N1 = " + n1);
            Console.WriteLine("N2 = " + n2);
            Console.WriteLine("N3 = " + n3);

            Console.WriteLine("Q1 = " + q1);
            Console.WriteLine("Q2 = " + q2);
            Console.WriteLine("Q3 = " + q3);

            Console.WriteLine("Q0 = " + q0);

            Console.Read();
        }
    }
}
