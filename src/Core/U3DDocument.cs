using System.Collections.Generic;
using System.IO;
using System.Text;
using Universal3d.Core.Extensions;
using Universal3d.Core.IO;
using Universal3d.Core.Primitives;
using Universal3d.Core.Primitives.MaterialPrimitives;
using Universal3d.Core.Primitives.MeshPrimitives;
using Universal3d.Core.Primitives.NodePrimitives;
using Universal3d.Core.Primitives.TexturePrimitives;

namespace Universal3d.Core;
public class U3dDocument
{
    #region Properties

    public Dictionary<string, Material> Materials { get; } = [];
    public Dictionary<string, Mesh> Meshes { get; } = [];
    public Dictionary<string, Node> Nodes { get; } = [];
    public Dictionary<string, Shader> Shaders { get; } = [];
    public Dictionary<string, Texture> Textures { get; } = [];
    public Encoding TextEncoding => Header.CharacterEncoding.ToSystemEncoding();
    public U3dHeader Header { get; } = new();

    #endregion Properties

    #region Methods

    public bool TryAddNode(Node node)
    {
        if ((node == null) || string.IsNullOrEmpty(node.Name) || Nodes.ContainsKey(node.Name))
        {
            return false;
        }
        if (!string.IsNullOrEmpty(node.Mesh) && !Meshes.ContainsKey(node.Mesh))
        {
            return false;
        }
        if (!string.IsNullOrEmpty(node.Parent) && !Nodes.ContainsKey(node.Parent))
        {
            return false;
        }
        Nodes.Add(node.Name, node);
        return true;
    }

    public bool TryAddMesh(Mesh mesh)
    {
        if ((mesh == null) || string.IsNullOrEmpty(mesh.Name) || Meshes.ContainsKey(mesh.Name))
        {
            return false;
        }
        if (string.IsNullOrEmpty(mesh.Shader) || !Shaders.ContainsKey(mesh.Shader))
        {
            return false;
        }
        if (!mesh.IsTrianglesCorrect())
        {
            return false;
        }
        Meshes.Add(mesh.Name, mesh);
        return true;
    }

    public bool TryAddMaterial(Material material)
    {
        if ((material == null) || string.IsNullOrEmpty(material.Name) || Materials.ContainsKey(material.Name))
        {
            return false;
        }
        Materials.Add(material.Name, material);
        return true;
    }

    public bool TryAddShader(Shader shader)
    {
        if ((shader == null) || string.IsNullOrEmpty(shader.Name) || Shaders.ContainsKey(shader.Name))
        {
            return false;
        }
        if (string.IsNullOrEmpty(shader.Material) || !Materials.ContainsKey(shader.Material))
        {
            return false;
        }
        if (!string.IsNullOrEmpty(shader.Texture) && !Textures.ContainsKey(shader.Texture))
        {
            return false;
        }
        Shaders.Add(shader.Name, shader);
        return true;
    }

    public bool TryAddTexture(Texture texture)
    {
        if ((texture == null) || string.IsNullOrEmpty(texture.Name) || Textures.ContainsKey(texture.Name))
        {
            return false;
        }
        if (texture.Image.Length == 0)
        {
            return false;
        }
        if (texture.ImageFormat == ImageFormat.Invalid)
        {
            return false;
        }
        Textures.Add(texture.Name, texture);
        return true;
    }

    public void Save(Stream stream, bool leaveOpen = true) => DocumentWriter.Save(this, stream, leaveOpen);

    public void Save(string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Create);
        Save(stream);
    }

    /// <summary>
    /// WIP: Only header handles for the moment.
    /// </summary>
    public static U3dDocument Load(Stream stream, bool leaveOpen = true) => DocumentReader.Read(stream, leaveOpen);

    /// <summary>
    /// WIP: Only header handles for the moment.
    /// </summary>
    public static U3dDocument Load(string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Open);
        return Load(stream, false);
    }

    #endregion Methods
}
