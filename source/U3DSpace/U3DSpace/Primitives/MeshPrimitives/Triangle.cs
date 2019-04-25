using System.Collections;

namespace U3DSpace.Primitives.MeshPrimitives
{
    public class Triangle : IEnumerable, IEnumerator
    {
        #region Fields

        private short _position;

        #endregion Fields

        #region Constructors

        public Triangle()
        {
            A = new Corner();
            B = new Corner();
            C = new Corner();
            _position = -1;
        }

        public Triangle(Corner a, Corner b, Corner c)
        {
            A = new Corner(a);
            B = new Corner(b);
            C = new Corner(c);
            _position = -1;
        }

        public Triangle(Triangle t)
        {
            A = new Corner(t.A);
            B = new Corner(t.B);
            C = new Corner(t.C);
        }

        #endregion Constructors

        #region Properties

        public Corner A { get; internal set; }

        public Corner B { get; internal set; }

        public Corner C { get; internal set; }

        public object Current
        {
            get
            {
                if (_position == 0)
                {
                    return A;
                }
                else if (_position == 1)
                {
                    return B;
                }
                else
                {
                    return C;
                }
            }
        }

        #endregion Properties

        #region Methods

        public static bool operator !=(Triangle a, Triangle b)
        {
            return !(a == b);
        }

        public static bool operator ==(Triangle a, Triangle b)
        {
            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Triangle triangle)
            {
                if (A == triangle.A &&
                    B == triangle.B &&
                    C == triangle.C)
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
            return (A.GetHashCode() * 3) + (B.GetHashCode() * 5) + (C.GetHashCode() * 7);
        }

        public IEnumerator GetEnumerator()
        {
            Reset();
            return this;
        }

        public bool MoveNext() => ++_position < 3;

        public void Reset() => _position = -1;

        #endregion Methods
    }
}