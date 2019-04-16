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

        #endregion Constructors

        #region Properties

        public float B { get; internal set; }
        public float G { get; internal set; }
        public float R { get; internal set; }

        #endregion Properties

        #region Methods

        public override bool Equals(object obj)
        {
            if (obj is Color color)
            {
                if (R == color.R &&
                    G == color.G &&
                    B == color.B)
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