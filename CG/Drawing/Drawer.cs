using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CG.Painter
{
    public static class Drawer
    {
        public static void DrawLine(List<Vector3> vectorSet, Color lineColor)
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(lineColor);

            var beginVector = vectorSet.First();

            foreach (var endVector in vectorSet.Skip(1))
            {
                GL.Vertex3(beginVector);
                GL.Vertex3(endVector);
                beginVector = endVector;
            }
            GL.End();
        }
    }
}