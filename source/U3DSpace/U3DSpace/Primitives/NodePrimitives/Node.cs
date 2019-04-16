namespace U3DSpace.Primitives.NodePrimitives
{
    public class Node
    {
        #region Constructors

        public Node()
        {
            Name = string.Empty;
            Mesh = string.Empty;
            Parent = string.Empty;
            Transformation = new Matrix4();
        }

        public Node(string name, string mesh, string parent, Matrix4 transformation)
        {
            Name = name;
            Mesh = mesh;
            Parent = parent;
            Transformation = transformation;
        }

        #endregion Constructors

        #region Properties

        /// <summary>Use for identify this Node</summary>
        public string Name { get; internal set; }

        /// <summary>Name of the Mesh from U3DDocument</summary>
        public string Mesh { get; internal set; }

        /// <summary>Name of the Node from U3DDocument</summary>
        public string Parent { get; internal set; }

        /// <summary>Transformation relative to parent</summary>
        public Matrix4 Transformation { get; internal set; }

        #endregion Properties

        #region Methods

        public static bool operator !=(Node a, Node b)
        {
            return !(a == b);
        }

        public static bool operator ==(Node a, Node b)
        {
            return a.Equals(b);
        }

        public override int GetHashCode()
        {
            return (Name.GetHashCode() * 3) + (Mesh.GetHashCode() * 5) + (Parent.GetHashCode() * 7) + (Transformation.GetHashCode() * 11);
        }

        public override bool Equals(object obj)
        {
            if (obj is Node node)
            {
                if (Name == node.Name &&
                    Mesh == node.Mesh &&
                    Parent == node.Parent &&
                    Transformation == node.Transformation)
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

        #endregion Methods
    }
}