using System;

namespace Universal3d.Core.Primitives.MaterialPrimitives;
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

    public Color(double red, double green, double blue)
    {
        R = (float)red;
        G = (float)green;
        B = (float)blue;
    }

    public Color(float value)
    {
        R = value;
        G = value;
        B = value;
    }

    public Color(double value)
    {
        R = (float)value;
        G = (float)value;
        B = (float)value;
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
        var tol = 5e-5;

        if (obj is not Color color)
            return false;

        return
            (Math.Abs(R - color.R) < tol) &&
            (Math.Abs(G - color.G) < tol) &&
            (Math.Abs(B - color.B) < tol);
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
