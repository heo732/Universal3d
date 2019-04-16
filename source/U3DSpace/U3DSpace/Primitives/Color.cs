using System;

namespace U3DSpace.Primitives
{
    public class Color
    {
        #region Constructors

        public Color()
        {
            R = 1.0f;
            G = 1.0f;
            B = 1.0f;
        }

        public Color(float red, float green, float blue)
        {
            R = red;
            G = green;
            B = blue;
        }

        public Color(float value)
        {
            R = value;
            G = value;
            B = value;
        }

        public Color(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
        }

        #endregion Constructors

        #region Properties

        public float B { get; internal set; }
        public float G { get; internal set; }
        public float R { get; internal set; }

        #endregion Properties

        #region Methods

        public static bool operator !=(Color a, Color b)
        {
            return !(a == b);
        }

        public static bool operator ==(Color a, Color b)
        {
            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Color color)
            {
                if ((Math.Abs(R - color.R) < 0.00005) &&
                    (Math.Abs(G - color.G) < 0.00005) &&
                    (Math.Abs(B - color.B) < 0.00005))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (int)((R * 3) + (G * 5) + (B * 7));
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", R, G, B);
        }

        #endregion Methods
    }
}