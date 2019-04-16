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
    }
}