using System.Collections.Generic;
using U3DSpace.Primitives;

namespace U3DSpace
{
    public class U3DDocument
    {
        #region Constructors

        public U3DDocument()
        {
            Materials = new List<Material>();
            Meshes = new List<Mesh>();
            Nodes = new List<Node>();
            Textures = new List<Texture>();
        }

        #endregion Constructors

        #region Properties

        public List<Material> Materials { get; internal set; }
        public List<Mesh> Meshes { get; internal set; }
        public List<Node> Nodes { get; internal set; }
        public List<Texture> Textures { get; internal set; }

        #endregion Properties
    }
}