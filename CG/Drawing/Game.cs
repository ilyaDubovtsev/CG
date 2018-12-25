using System;
using System.Collections.Generic;
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
            GL.Flush();
            var vectors = new List<Vector3>
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 1, 0),
                new Vector3(1, 1, 0),
                new Vector3(1, (float) 0.8, 0),
                new Vector3((float) 0.2, 1, 0),
                new Vector3(1, (float) 0.1, 0),
                new Vector3(0, 0, 0),
            };
            Drawer.DrawLine(vectors, Color.Red);
            

            Window.SwapBuffers();
        }

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}