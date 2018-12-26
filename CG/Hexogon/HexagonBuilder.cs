using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CG.HexagonFolder
{
	public class HexagonBuilder : IHexagonBuilder
	{
		private static double Radius;
		private static double DeltaX;
		private int CurrentTick;
		private const double DeltaPhi = Math.PI / 6;
		private static Hexagon CurrentHexagon;


		public HexagonBuilder(int width, int height)
		{
			CurrentTick = 0;

			var horizontalR = 0.05 * width;
			var verticalR = 0.2 * height;

			Radius = (horizontalR > verticalR) ? horizontalR / width : verticalR / height;

			DeltaX = (Math.PI / 6 * Radius);
			CurrentHexagon = new Hexagon
				{
					Center = new Vector3(0, 0, 0),
					BorderPoints = new List<Vector3>()
				};

			for (int i = 0; i < 6; i++)
			{
				CurrentHexagon.BorderPoints.Add(new Vector3((float)(Radius * Math.Cos(i * Math.PI / 3)), (float)(Radius * Math.Sin(i * Math.PI / 3)), 0));
			}
		}

		public Hexagon GetNext()
		{
			if (CurrentHexagon.Center.X - 1 + DeltaX * CurrentTick > 1 + Radius)
			{
				CurrentTick = 0;
			}

			CurrentTick += 1;

			var newBorder = new List<Vector3>();
			for (int i = 0; i < 6; i++)
			{
				newBorder.Add(TurnPoint(CurrentHexagon.BorderPoints[i], (float)DeltaPhi * CurrentTick));
				newBorder[newBorder.Count - 1] = new Vector3((float)(newBorder[newBorder.Count - 1].X - 1 + DeltaX * CurrentTick), newBorder[newBorder.Count - 1].Y, 0);
			}

			return new Hexagon()
			{
				Center = new Vector3((float)(CurrentHexagon.Center.X - 1 + DeltaX * CurrentTick), CurrentHexagon.Center.Y, CurrentHexagon.Center.Z),
				BorderPoints = newBorder
			};
		}

		private Vector3 TurnPoint(Vector3 vector, float angle)
		{
			return MatrixMultiplication(vector, CreateTurnMatrix(angle));
		}

		private Vector3 MoveVector(Vector3 vector, int deltaX)
		{
			return new Vector3((float)(vector.X + deltaX), (float)vector.Y, vector.Z);
		}

		private static float[,] CreateTurnMatrix(double angle)
		{
			var matrix = new float[3, 3];
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					matrix[i, j] = 0;
				}
			}
			matrix[0, 0] = (float)Math.Cos(angle);
			matrix[0, 1] = (float)Math.Sin(angle);
			matrix[1, 0] = (float)-Math.Sin(angle);
			matrix[1, 1] = (float)Math.Cos(angle);
			matrix[2, 2] = 1;
			return matrix;
		}

		private Vector3 MatrixMultiplication(Vector3 vector, float[,] turnMatrix)
		{

			var result = new Vector3();
			result.X = vector.X * turnMatrix[0, 0] + vector.Y * turnMatrix[0, 1] + vector.Z * turnMatrix[0, 2];
			result.Y = vector.X * turnMatrix[1, 0] + vector.Y * turnMatrix[1, 1] + vector.Z * turnMatrix[1, 2];
			result.Z = vector.X * turnMatrix[2, 0] + vector.Y * turnMatrix[2, 1] + vector.Z * turnMatrix[2, 2];
			return result;
		}
	}
}