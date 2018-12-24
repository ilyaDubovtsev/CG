using System;
using OpenTK;
using System.ComponentModel;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace CG.Painter
{
    public class Game
    {
        public GameWindow Window;

        public Game(GameWindow window)
        {
            Window = window;

            Window.Load += WindowLoad;
            Window.RenderFrame += WindowRenderFrame;
            Window.UpdateFrame += WindowUpdateFrame;
            Window.Closing += WindowClose;
        }

        private void WindowLoad(object sender, EventArgs e)
        {
        }

        private void WindowUpdateFrame(object sender, FrameEventArgs e)
        {
        }

        private void WindowRenderFrame(object sender, FrameEventArgs e)
        {
            GL.ClearColor(Color.FromArgb(5,5,25));
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(1, 1, 0);
            GL.Vertex3(-1, 1, 0);
            GL.End();
            
            GL.Flush();
            Window.SwapBuffers();
        }

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}