using System;

namespace U3DSpace.Primitives.MeshPrimitives
{
    public class Vector2
    {
        #region Constructors

        public Vector2()
        {
            X = 0.0f;
            Y = 0.0f;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2(float value)
        {
            X = value;
            Y = value;
        }

        public Vector2(Vector2 vector2)
        {
            X = vector2.X;
            Y = vector2.Y;
        }

        #endregion Constructors

        #region Properties

        public float X { get; internal set; }
        public float Y { get; internal set; }

        #endregion Properties

        #region Methods

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2 vector2)
            {
                if ((Math.Abs(X - vector2.X) < 0.00005) &&
                    (Math.Abs(Y - vector2.Y) < 0.00005))
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
            return (int)((X * 3) + (Y * 5));
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", X, Y);
        }

        #endregion Methods
    }
}