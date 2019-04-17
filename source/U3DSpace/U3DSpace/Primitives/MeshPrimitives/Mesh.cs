using System.Collections.Generic;

namespace U3DSpace.Primitives.MeshPrimitives
{
    public class Mesh
    {
        #region Constructors

        public Mesh()
        {
            Name = string.Empty;
            Shader = string.Empty;
            Positions = new List<Vector3>();
            Normals = new List<Vector3>();
            TextureCoordinates = new List<Vector2>();
            Triangles = new List<Triangle>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>Use for identify this Mesh</summary>
        public string Name { get; internal set; }

        /// <summary>Name of the Shader from U3DDocument</summary>
        public string Shader { get; internal set; }

        public List<Vector3> Positions { get; internal set; }

        public List<Vector3> Normals { get; internal set; }

        public List<Vector2> TextureCoordinates { get; internal set; }

        public List<Triangle> Triangles { get; internal set; }

        #endregion Properties

        #region Methods

        public bool IsTrianglesCorrect()
        {
            bool result = true;
            foreach (var triangle in Triangles)
            {
                if (triangle.A.Position >= Positions.Count ||
                    triangle.B.Position >= Positions.Count ||
                    triangle.C.Position >= Positions.Count ||
                    triangle.A.Normal >= Normals.Count ||
                    triangle.B.Normal >= Normals.Count ||
                    triangle.C.Normal >= Normals.Count ||
                    triangle.A.TextureCoordinate >= TextureCoordinates.Count ||
                    triangle.B.TextureCoordinate >= TextureCoordinates.Count ||
                    triangle.C.TextureCoordinate >= TextureCoordinates.Count)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        #endregion Methods
    }
}