using System.Collections.Generic;
using U3DSpace.Primitives;
using U3DSpace.Primitives.MaterialPrimitives;
using U3DSpace.Primitives.MeshPrimitives;
using U3DSpace.Primitives.NodePrimitives;
using U3DSpace.Primitives.TexturePrimitives;

namespace U3DSpace
{
    public class U3DDocument
    {
        #region Constructors

        public U3DDocument()
        {
            Shaders = new Dictionary<string, Shader>();
            Materials = new Dictionary<string, Material>();
            Meshes = new Dictionary<string, Mesh>();
            Nodes = new Dictionary<string, Node>();
            Textures = new Dictionary<string, Texture>();
        }

        #endregion Constructors

        #region Properties

        public Dictionary<string, Material> Materials { get; internal set; }
        public Dictionary<string, Mesh> Meshes { get; internal set; }
        public Dictionary<string, Node> Nodes { get; internal set; }
        public Dictionary<string, Shader> Shaders { get; internal set; }
        public Dictionary<string, Texture> Textures { get; internal set; }

        #endregion Properties
    }
}