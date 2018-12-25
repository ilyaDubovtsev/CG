using System;
using OpenTK;
using System.ComponentModel;
using System.Drawing;
using CG.HexogonFolder;
using OpenTK.Graphics.OpenGL;

namespace CG.Painter
{
    public class Game
    {
        private static GameWindow Window;
        private static HexagonBuilder HexagonBuilder;

        public Game(GameWindow window)
        {
            Window = window;
            HexagonBuilder = new HexagonBuilder(window.Width, window.Height);

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

            var hexagon = HexagonBuilder.GetNext();
            Drawer.DrawLine(hexagon.BorderPoints, Color.Red);
            
            GL.Flush();
            Window.SwapBuffers();
        }

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}