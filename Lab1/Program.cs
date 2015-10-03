using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace Lab1 {
	class Program {
		static void Main() {
			var l = new double[] { 100, 0, 150, 0, 50, 0, 0, 200, 300, 0 };
			var k = new double[] { 0, 50, 0, 75, 0, 100, 75, 0, 0, 50 };
			var nf = new int[] { 1, 0, 2, 0, 1, 0, 0, 2, 3, 0 };

			var probabilityMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
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
				probabilityMatrix[i, i] = probabilityMatrix[i, i] - 1;

			var solutions = probabilityMatrix.Solve(constantTermsVector).ToArray();
			var Q = k.Select(((ki, i) => ki * solutions[i])).Sum();
			double[] Nh = { 0, 0, 0 };

			for (var i = 0; i < nf.Length; i++) {
				switch (nf[i]) {
					case 1:
						Nh[0] += solutions[i];
						break;
					case 2:
						Nh[1] += solutions[i];
						break;
					case 3:
						Nh[2] += solutions[i];
						break;
				}
			}

			double[] Qh = {0, 0, 0};
			double N = 0;
			for (var i = 0; i < nf.Length; i++) {
				switch (nf[i]) {
					case 1:
						Qh[0] += solutions[i] * l[i];
						break;
					case 2:
						Qh[1] += solutions[i] * l[i];
						break;
					case 3:
						Qh[2] += solutions[i] * l[i];
						break;
					default:
						N += solutions[i];
						break;
				}
			}

			for (var i = 0; i < Qh.Length; i++)
				Qh[i] = Qh[i] * (1 / Nh[i]);

			var Q0 = Q / N;

			for (var i = 0; i < solutions.Length; i++)
				Console.WriteLine("n[" + (i + 1) + "] = " + solutions[i]);

			Console.WriteLine(Environment.NewLine + "Q = " + Q);

			for (var i = 0; i < Nh.Length; i++)
				Console.WriteLine("N" + (i + 1) + " = " + Nh[i] + ", Q" + (i + 1) + " = " + Qh[i]);

			Console.WriteLine(Environment.NewLine + "Q0 = " + Q0);
			Console.WriteLine(Environment.NewLine + "N = " + N);
			Console.Read();
		}
	}
}