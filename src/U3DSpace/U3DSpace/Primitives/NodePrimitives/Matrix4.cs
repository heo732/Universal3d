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
                Values = new float[16];
                Array.Copy(values, Values, 16);
            }
            else
            {
                Values = new float[16];
            }
        }

        public Matrix4(double[] values)
        {
            if (values.Length == 16)
            {
                Values = new float[16];
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

        public Matrix4(double value)
        {
            Values = new float[16];
            for (int i = 0; i < 16; i++)
            {
                Values[i] = (float)value;
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

        public float this[int i]
        {
            get
            {
                if ((i < 0) || (i > 3))
                {
                    throw new ArgumentOutOfRangeException();
                }
                return Values[i];
            }
            set
            {
                if ((i < 0) || (i > 3))
                {
                    throw new ArgumentOutOfRangeException();
                }
                Values[i] = value;
            }
        }

        public float this[int row, int col]
        {
            get
            {
                if ((row < 0) || (row > 3))
                {
                    throw new ArgumentOutOfRangeException();
                }
                if ((col < 0) || (col > 3))
                {
                    throw new ArgumentOutOfRangeException();
                }
                return Values[(row * 4) + col];
            }
            set
            {
                if ((row < 0) || (row > 3))
                {
                    throw new ArgumentOutOfRangeException();
                }
                if ((col < 0) || (col > 3))
                {
                    throw new ArgumentOutOfRangeException();
                }
                Values[(row * 4) + col] = value;
            }
        }

        public static bool operator !=(Matrix4 a, Matrix4 b)
        {
            return !(a == b);
        }

        public static bool operator ==(Matrix4 a, Matrix4 b)
        {
            return a.Equals(b);
        }

        public static Matrix4 GetIdentityMatrix()
        {
            return new Matrix4(new float[] {
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1,
            });
        }

        public static Matrix4 ChangeOrder(Matrix4 m)
        {
            return new Matrix4(new float[] {
                m[0], m[4], m[8], m[12],
                m[1], m[5], m[9], m[13],
                m[2], m[6], m[10], m[14],
                m[3], m[7], m[11], m[15]
            });
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

        public float[] ToArray()
        {
            var result = new float[16];
            Array.Copy(Values, result, 16);
            return result;
        }

        #endregion Methods
    }
}