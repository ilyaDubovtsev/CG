using System;
using System.Collections.Generic;
using OpenTK;
using System.Drawing;
using System.Linq;
using CG.HexagonFolder;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace CG.Painter
{
    public class Game
    {
        private static GameWindow Window;
        private static HexagonBuilder HexagonBuilder;
        private static int ChangeFrameNumber;
        private static int CurrentFrame;
        private Hexagon CurrentHexagon;

        public Game(GameWindow window)
        {
            Window = window;
            HexagonBuilder = new HexagonBuilder(window.Width, window.Height, 0, Vector3.Zero);
            CurrentFrame = 0;
            ChangeFrameNumber = 6;
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

            if (CurrentFrame % ChangeFrameNumber == 0)
            {
                CurrentHexagon = HexagonBuilder.GetNext();
            }

            Drawer.DrowBackground(Color4.LightSkyBlue, Color4.RoyalBlue);
            Drawer.DrawLine(CurrentHexagon.BorderPoints, Color.DimGray);
            Drawer.DrawHexagon(CurrentHexagon.BorderPoints, CurrentHexagon.Center, Palette.GetColorByNumber);
            Drawer.DrawLine(new List<Vector3>{CurrentHexagon.Center, CurrentHexagon.BorderPoints.First()}, Color.Red);
            Drawer.DrawLine(new List<Vector3>{new Vector3(-1 , 0 , 0), new Vector3(1 , 0 , 0)}, Color4.Red);

            GL.Flush();
            Window.SwapBuffers();

            CurrentFrame++;
        }

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}