using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace CG.Painter
{
    public static class Drawer
    {
        public static void DrawHexagon(List<Vector3> vectorSet, Vector3 center, Func<int, Color4> getColor)
        {
            var count = vectorSet.Count;
            for (int i = 0; i < count; i++)
            {
                var triangleVectors = new List<Vector3> {
                    center,
                    vectorSet[i],
                    vectorSet[(i + 1) % count],
                };
                DrawTriangle(triangleVectors, getColor(i));
            }
        }
        
        public static void DrawTriangle(List<Vector3> vectorSet, Color4 fillColor)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color4(fillColor);
            vectorSet.ForEach(GL.Vertex3);
            GL.End();
        }
        
        public static void DrawLine(List<Vector3> vectorSet, Color4 lineColor)
        {
            GL.Begin(PrimitiveType.Lines);
            var beginVector = vectorSet.First();

            foreach (var endVector in vectorSet.Skip(1))
            {
                GL.Color4(lineColor);
                GL.Vertex3(beginVector);
                GL.Vertex3(endVector);
                beginVector = endVector;
            }
            
            GL.End();
        }

        public static void DrowRectangle(List<Vector3> vectorSet, List<Color4> colorSet)
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color4(colorSet[0]);
            GL.Vertex3(vectorSet[0]);
            GL.Color4(colorSet[1]);
            GL.Vertex3(vectorSet[1]);
            GL.Color4(colorSet[2]);
            GL.Vertex3(vectorSet[2]);
            GL.Color4(colorSet[3]);
            GL.Vertex3(vectorSet[3]);
            GL.End();
        }

        public static void DrowBackground(List<Color4> colorSet)
        {
            var rectangleVectors = new List<Vector3>
            {
                new Vector3(-1, 1, 0),     
                new Vector3(1, 1, 0),     
                new Vector3(1, -1, 0),     
                new Vector3(-1, -1, 0),     
            };
            
            DrowRectangle(rectangleVectors, colorSet);
        }
    }
}