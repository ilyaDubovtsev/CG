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
        private static List<HexagonBuilder> SmallHexagonBuilders;
        private static List<Hexagon> SmallHexagons;
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
            HexagonBuilder = new HexagonBuilder(Window.Width, Window.Height, 0, new Vector3(0, 0, 0));
            SmallHexagonBuilders = new List<HexagonBuilder>();
            CurrentHexagon = HexagonBuilder.GetNext();
            foreach (var borderPoint in CurrentHexagon.BorderPoints)
            {
                SmallHexagonBuilders.Add(new HexagonBuilder(Window.Width, Window.Height, 1, new Vector3(0, borderPoint.Y, 0)));
                SmallHexagonBuilders.Add(new HexagonBuilder(Window.Width, Window.Height, 1, new Vector3(borderPoint.Y, 0, 0)));
            }
            
            SmallHexagonBuilders.Add(new HexagonBuilder(Window.Width, Window.Height, 1, new Vector3(0, 0, 0)));


            SmallHexagons = new List<Hexagon>();
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
                SmallHexagons = new List<Hexagon>();
                foreach (var smallHexagonBuilder in SmallHexagonBuilders)
                {
                    SmallHexagons.Add(smallHexagonBuilder.GetNext());
                }
                
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
            Drawer.DrawHexagon(CurrentHexagon.BorderPoints, CurrentHexagon.Center, Palette.GetColorByNumber);
            Drawer.DrawLine(CurrentHexagon.BorderPoints, Color.DimGray);
            Drawer.DrawLine(new List<Vector3>{CurrentHexagon.Center, CurrentHexagon.BorderPoints.First()}, Color.Red);
            Drawer.DrawLine(new List<Vector3>{new Vector3(-1 , 0 , 0), new Vector3(1 , 0 , 0)}, Color4.Red);
            
            foreach (var currentSmallHex in SmallHexagons)
            {
                Drawer.DrawHexagon(currentSmallHex.BorderPoints, currentSmallHex.Center, Palette.GetRandomColor);
            }

            GL.Flush();
            Window.SwapBuffers();

            CurrentFrame++;
        }

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}