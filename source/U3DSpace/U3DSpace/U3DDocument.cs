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

        #region Methods

        public bool TryToAddNode(Node node)
        {
            if (!Nodes.ContainsKey(node.Name))
            {
                Nodes.Add(node.Name, node);
                return true;
            }
            return false;
        }

        public bool TryToAddMesh(Mesh mesh)
        {
            if (!Meshes.ContainsKey(mesh.Name))
            {
                Meshes.Add(mesh.Name, mesh);
                return true;
            }
            return false;
        }

        public bool TryToAddMaterial(Material material)
        {
            if (!Materials.ContainsKey(material.Name))
            {
                Materials.Add(material.Name, material);
                return true;
            }
            return false;
        }

        public bool TryToAddShader(Shader shader)
        {
            if (!Shaders.ContainsKey(shader.Name))
            {
                Shaders.Add(shader.Name, shader);
                return true;
            }
            return false;
        }

        public bool TryToAddTexture(Texture texture)
        {
            if (!Textures.ContainsKey(texture.Name))
            {
                Textures.Add(texture.Name, texture);
                return true;
            }
            return false;
        }

        #endregion Methods
    }
}