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
        private List<Color4> BackgroundColorSet;
        private MagicColor ColorMaker1;
        private MagicColor ColorMaker2;
        private MagicColor ColorMaker3;
        private MagicColor ColorMaker4;

        public Game(GameWindow window)
        {
            Window = window;
            HexagonBuilder = new HexagonBuilder(window.Width, window.Height, 1, new Vector3(0.1f, 0.1f, 0));
            CurrentFrame = 0;
            ChangeFrameNumber = 6;
            ColorMaker1 = new MagicColor(Color4.Coral);
            ColorMaker2 = new MagicColor(Color4.LightYellow);
            ColorMaker3 = new MagicColor(Color4.MintCream);
            ColorMaker4 = new MagicColor(Color4.RoyalBlue);
            
            BackgroundColorSet = new List<Color4>
            {
                ColorMaker1.GetColor(),
                ColorMaker2.GetColor(),
                ColorMaker3.GetColor(),
                ColorMaker4.GetColor(),
            };
            
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
            GL.Clear(ClearBufferMask.ColorBufferBit);

            if (CurrentFrame % ChangeFrameNumber == 0)
            {
                CurrentHexagon = HexagonBuilder.GetNext();
            }

            BackgroundColorSet = new List<Color4>
            {
                ColorMaker1.GetColor(),
                ColorMaker2.GetColor(),
                ColorMaker3.GetColor(),
                ColorMaker4.GetColor(),
            };
            Drawer.DrowBackground(BackgroundColorSet);
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