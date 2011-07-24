﻿
namespace Terraria_Server.Misc
{
    public class Color
    {
        public int R;
        public int G;
        public int B;
        public int A;

        public Color() { }

        public Color(int r, int g, int b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public Color(int r, int g, int b, int a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public Color(System.Drawing.Color Colour)
        {
            this.R = Colour.R;
            this.G = Colour.G;
            this.B = Colour.B;
            this.A = Colour.A;
        }
    }
}
