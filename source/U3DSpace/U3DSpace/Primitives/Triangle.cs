namespace U3DSpace.Primitives
{
    public class Triangle
    {
        #region Constructors

        public Triangle()
        {
            A = new Corner();
            B = new Corner();
            C = new Corner();
        }

        public Triangle(Corner a, Corner b, Corner c)
        {
            A = new Corner(a);
            B = new Corner(b);
            C = new Corner(c);
        }

        #endregion Constructors

        #region Properties

        public Corner A { get; internal set; }

        public Corner B { get; internal set; }

        public Corner C { get; internal set; }

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

        #endregion Methods
    }
}