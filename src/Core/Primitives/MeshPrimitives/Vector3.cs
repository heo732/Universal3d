using System;

namespace Universal3d.Core.Primitives.MeshPrimitives;
public class Vector3
{
    #region Constructors

    public Vector3()
    {
        X = 0.0f;
        Y = 0.0f;
        Z = 0.0f;
    }

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector3(double x, double y, double z)
    {
        X = (float)x;
        Y = (float)y;
        Z = (float)z;
    }

    public Vector3(float value)
    {
        X = value;
        Y = value;
        Z = value;
    }

    public Vector3(double value)
    {
        X = (float)value;
        Y = (float)value;
        Z = (float)value;
    }

    public Vector3(Vector3 vector3)
    {
        X = vector3.X;
        Y = vector3.Y;
        Z = vector3.Z;
    }

    #endregion Constructors

    #region Properties

    public float X { get; internal set; }
    public float Y { get; internal set; }
    public float Z { get; internal set; }

    #endregion Properties

    #region Methods

    public static bool operator !=(Vector3 a, Vector3 b)
    {
        return !(a == b);
    }

    public static bool operator ==(Vector3 a, Vector3 b)
    {
        return a.Equals(b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Vector3 vector3)
        {
            if ((Math.Abs(X - vector3.X) < 0.00005) &&
                (Math.Abs(Y - vector3.Y) < 0.00005) &&
                (Math.Abs(Z - vector3.Z) < 0.00005))
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
        return (int)((X * 3) + (Y * 5) + (Z * 7));
    }

    public override string ToString()
    {
        return string.Format("{0} {1} {2}", X, Y, Z);
    }

    #endregion Methods
}
