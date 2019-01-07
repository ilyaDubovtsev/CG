using OpenTK.Graphics;

namespace CG.Painter
{
    public class MagicColor
    {
        private Color4 Color;
        private ColorSettings Settings;
        private float Step;

        public MagicColor(Color4 color, float step = (float)0.01)
        {
            Color = color;
            Step = step;
            Settings = new ColorSettings
            {
                BInc = true,
                GInc = true,
                RInc = true
            };
        }

        public Color4 GetColor()
        {
            float r = 0;
            if (Settings.RInc)
            {
                r = Color.R + Step;
                if (Color.R >= 1)
                    Settings.RInc = false;
            }
            else
            {
                r = Color.R - Step;
                if (Color.R <= 0.2)
                    Settings.RInc = true;
            }

            float g = 0;
            if (Settings.GInc)
            {
                g = Color.G + Step;
                if (Color.G >= 1)
                    Settings.GInc = false;
            }
            else
            {
                g = Color.G - Step;
                if (Color.G <= 0.2)
                    Settings.GInc = true;
            }

            float b = 0;
            if (Settings.BInc)
            {
                b = Color.B + Step;
                if (Color.B >= 1)
                    Settings.BInc = false;
            }
            else
            {
                b = Color.B - Step;
                if (Color.B <= 0.2)
                    Settings.BInc = true;
            }

            Color = new Color4(r, g, b, 1);
            return Color;
        }
    }
    
    internal class ColorSettings
    {
        public bool RInc { get; set; }
        public bool GInc { get; set; }
        public bool BInc { get; set; }
    }
}