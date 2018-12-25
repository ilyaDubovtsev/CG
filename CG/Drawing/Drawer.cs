using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CG.Painter
{
    public static class Drawer
    {
        public static void DrawHexagon(List<Vector3> vectorSet, Vector3 center, Func<int, Color> getColor)
        {
            for (int i = 0; i < vectorSet.Count / 2; i++)
            {
                var triangleVectors = new List<Vector3> {
                    center,
                    vectorSet[i * 2],
                    vectorSet[i * 2 + 1],
                };
                DrawTriangle(triangleVectors, getColor(i));
            }
        }
        
        public static void DrawTriangle(List<Vector3> vectorSet, Color fillColor)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(fillColor);
            vectorSet.ForEach(GL.Vertex3);
            GL.End();
        }
        
        public static void DrawLine(List<Vector3> vectorSet, Color lineColor)
        {
            GL.Begin(PrimitiveType.Lines);
            var beginVector = vectorSet.First();

            foreach (var endVector in vectorSet.Skip(1))
            {
                GL.Color3(lineColor);
                GL.Vertex3(beginVector);
                GL.Vertex3(endVector);
                beginVector = endVector;
            }
            
            GL.End();
        }
    }
}