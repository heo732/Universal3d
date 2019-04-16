using System;

namespace U3DSpace.Primitives.NodePrimitives
{
    public class Matrix4
    {
        #region Constructors

        public Matrix4()
        {
            Values = new float[16];
        }

        public Matrix4(float[] values)
        {
            if (values.Length == 16)
            {
                Array.Copy(values, Values, 16);
            }
            else
            {
                Values = new float[16];
            }
        }

        public Matrix4(float value)
        {
            Values = new float[16];
            for (int i = 0; i < 16; i++)
            {
                Values[i] = value;
            }
        }

        public Matrix4(Matrix4 matrix4)
        {
            Values = new float[16];
            for (int i = 0; i < 16; i++)
            {
                Values[i] = matrix4.Values[i];
            }
        }

        #endregion Constructors

        #region Properties

        public float[] Values { get; internal set; }

        #endregion Properties

        #region Methods

        public static bool operator !=(Matrix4 a, Matrix4 b)
        {
            return !(a == b);
        }

        public static bool operator ==(Matrix4 a, Matrix4 b)
        {
            return a.Equals(b);
        }

        public override int GetHashCode()
        {
            int result = 0;
            foreach (var value in Values)
            {
                result += (int)(value * 11);
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is Matrix4 matrix4)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (Math.Abs(Values[i] - matrix4.Values[i]) < 0.00005f)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        #endregion Methods
    }
}