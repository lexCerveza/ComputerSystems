using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace Lab1
{
    class Program
    {
        static void Main()
        {
            // Input shit to calculate stuff ...
            var l = new double[] { };
            var k = new double[] {  };
            var numberFile = new int[] {  };

            var a = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                
            });

            var b = Vector<double>.Build.Dense(new double[] { -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
            var n = a.Transpose().Solve(b).ToArray();

            var q = k.Select(((ki, i) => ki * n[i])).Sum();

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

            double q1 = 0;
            double q2 = 0;
            double q3 = 0;
            double N = 0;
            for (var i = 0; i < numberFile.Length; i++)
            {
                switch (numberFile[i])
                {
                    case 1:
                        q1 += n[i] * l[i];
                        break;
                    case 2:
                        q2 += n[i] * l[i];
                        break;
                    case 3:
                        q3 += n[i] * l[i];
                        break;
                    default:
                        N += n[i];
                        break;
                }
            }

            q1 = q1 * (1 / n1);
            q2 = q2 * (1 / n2);
            q3 = q3 * (1 / n3);

            var q0 = q / N;

            Console.WriteLine("Q = " + q);

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
