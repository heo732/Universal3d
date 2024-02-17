using System.IO;
using Universal3d.Core.Enums;
using Universal3d.Core.IO.BlockIO;
using Universal3d.Core.Primitives;
using Universal3d.Core.Primitives.MaterialPrimitives;
using Universal3d.Core.Primitives.MeshPrimitives;
using Universal3d.Core.Primitives.NodePrimitives;
using Universal3d.Core.Primitives.TexturePrimitives;

namespace Universal3d.Core.IO;
internal static class DocumentWriter
{
    #region PublicMethods

    public static void Save(U3dDocument doc, Stream stream, bool leaveOpen = true)
    {
        using var writer = new BinaryWriter(stream, doc.TextEncoding, leaveOpen);
        writer.Write(GetHeaderBlock(doc).ToArray());
        WriteNodes(writer, doc);
        WriteDeclarationsOfMeshes(writer, doc);
        WriteShaders(writer, doc);
        WriteMaterials(writer, doc);
        WriteDeclarationsOfTextures(writer, doc);
        WriteContinuationsOfMeshes(writer, doc);
        WriteContinuationsOfTextures(writer, doc);
        stream.Flush();
    }

    #endregion PublicMethods

    #region PrivateMethods

    private static void WriteNodes(BinaryWriter writer, U3dDocument doc)
    {
        foreach (Node node in doc.Nodes.Values)
        {
            if (string.IsNullOrEmpty(node.Mesh))
            {
                writer.Write(GetGroupNodeModifierChain(node).ToArray());
            }
            else
            {
                writer.Write(GetNodeModifierChain(node, doc).ToArray());
            }
        }
    }

    private static void WriteDeclarationsOfMeshes(BinaryWriter writer, U3dDocument doc)
    {
        foreach (Mesh mesh in doc.Meshes.Values)
        {
            writer.Write(GetModelResourceModifierChain(mesh).ToArray());
        }
    }

    private static void WriteContinuationsOfMeshes(BinaryWriter writer, U3dDocument doc)
    {
        foreach (Mesh mesh in doc.Meshes.Values)
        {
            writer.Write(GetMeshContinuationBlock(mesh).ToArray());
        }
    }

    private static void WriteShaders(BinaryWriter writer, U3dDocument doc)
    {
        foreach (Shader shader in doc.Shaders.Values)
        {
            writer.Write(GetLitTextureShaderBlock(shader).ToArray());
        }
    }

    private static void WriteMaterials(BinaryWriter writer, U3dDocument doc)
    {
        foreach (Material material in doc.Materials.Values)
        {
            writer.Write(GetMaterialResourceBlock(material).ToArray());
        }
    }

    private static void WriteDeclarationsOfTextures(BinaryWriter writer, U3dDocument doc)
    {
        foreach (Texture texture in doc.Textures.Values)
        {
            writer.Write(GetTextureResourceModifierChain(texture).ToArray());
        }
    }

    private static void WriteContinuationsOfTextures(BinaryWriter writer, U3dDocument doc)
    {
        foreach (Texture texture in doc.Textures.Values)
        {
            writer.Write(GetTextureContinuationBlock(texture).ToArray());
        }
    }

    private static Block GetHeaderBlock(U3dDocument doc)
    {
        var w = new BlockWriter();
        w.WriteI32(0x00000000); // version
        w.WriteU32(0x00000004); // profile identifier (0x00000004 - No compression mode)
        w.WriteU32(36); // declaration size
        w.WriteU64(732); // file size
        w.WriteU32(106); // character encoding: 106 = UTF-8
        //Meta data.
        w.WriteMetaU32(1); // Key/Value Pair Count
        w.WriteMetaU32((uint)(U3dMetaItemAttributes.String | U3dMetaItemAttributes.DisplayKey | U3dMetaItemAttributes.DisplayValue)); // Key/Value Pair Attributes;
        w.WriteMetaString("Generator", doc.TextEncoding); // Key String
        w.WriteMetaString("This file created by Universal.3d library", doc.TextEncoding); // Value String
        return w.GetBlock(BlockType.Header);
    }

    private static Block GetGroupNodeBlock(Node node)
    {
        var w = new BlockWriter();
        w.WriteString(node.Name); // model node name
        w.WriteU32(1); // parent node count
        w.WriteString(node.Parent); // parent node name
        w.WriteArray(node.Transformation.ToArray()); // transformation
        return w.GetBlock(BlockType.GroupNode);
    }

    private static Block GetGroupNodeModifierChain(Node node)
    {
        var w = new BlockWriter();
        w.WriteString(node.Name); // modifier chain name
        w.WriteU32(0); // modifier chain type: 0 = node modifier chain
        w.WriteU32(0); // modifier chain attributes: 0 = neither bounding sphere nor
        // Bounding box info present.
        w.WritePadding();
        w.WriteU32(1); // modifier count in this chain
        w.WriteBlock(GetGroupNodeBlock(node));
        return w.GetBlock(BlockType.ModifierChain);
    }

    private static Block GetModelNodeBlock(Node node)
    {
        var w = new BlockWriter();
        w.WriteString(node.Name); // model node name
        w.WriteU32(1); // parent node count
        w.WriteString(node.Parent); // parent node name
        w.WriteArray(node.Transformation.ToArray()); // transformation
        w.WriteString(node.Mesh); // model resource name
        w.WriteU32(3); // visibility 3 = front and back
        return w.GetBlock(BlockType.ModelNode);
    }

    private static Block GetShadingModifierBlock(string nodeName, string shaderName)
    {
        var w = new BlockWriter();
        w.WriteString(nodeName); // shading modifier name
        w.WriteU32(1); // chain index
        w.WriteU32(1); // shading attributes
        w.WriteU32(1); // shading list count
        w.WriteU32(1); // shader count
        w.WriteString(shaderName); // shader name
        return w.GetBlock(BlockType.ShadingModifier);
    }

    private static Block GetNodeModifierChain(Node node, U3dDocument doc)
    {
        var w = new BlockWriter();
        w.WriteString(node.Name); // modifier chain name
        w.WriteU32(0); // modifier chain type: 0 = node modifier chain
        w.WriteU32(0); // modifier chain attributes: 0 = neither bounding sphere nor
        // Bounding box info present.
        w.WritePadding();
        w.WriteU32(2); // modifier count in this chain
        w.WriteBlock(GetModelNodeBlock(node));
        w.WriteBlock(GetShadingModifierBlock(node.Name, doc.Meshes[node.Mesh].Shader));
        return w.GetBlock(BlockType.ModifierChain);
    }

    private static Block GetMeshDeclarationBlock(Mesh mesh)
    {
        var w = new BlockWriter();
        w.WriteString(mesh.Name); // mesh name
        w.WriteU32(0); // chain index
        // Max mesh description.
        w.WriteU32(mesh.Normals.Count == 0 ? 1u : 0u); // mesh attributes: 1 = no normals
        w.WriteU32((uint)mesh.Triangles.Count); // face count
        w.WriteU32((uint)mesh.Positions.Count); // positions count
        w.WriteU32((uint)mesh.Normals.Count); // normal count
        w.WriteU32(0); // diffuse color count
        w.WriteU32(0); // specular color count
        w.WriteU32((uint)mesh.TextureCoordinates.Count); // texture coord count
        w.WriteU32(1); // shading count
        // Shading description.
        w.WriteU32(0); // shading attributes
        w.WriteU32(mesh.TextureCoordinates.Count == 0 ? 0u : 1u); // texture layer count
        if (mesh.TextureCoordinates.Count > 0)
        {
            w.WriteU32(2); // texture coord dimensions
        }
        w.WriteU32(0); // original shading id
        // Clod desc.
        w.WriteU32((uint)mesh.Positions.Count); // minimum resolution
        w.WriteU32((uint)mesh.Positions.Count); // maximum resolution
        // Resource Description.
        w.WriteU32(300); // position quality factor
        w.WriteU32(300); // normal quality factor
        w.WriteU32(300); // texture coord quality factor
        w.WriteF32(0.01f); // position inverse quant
        w.WriteF32(0.01f); // normal inverse quant
        w.WriteF32(0.01f); // texture coord inverse quant
        w.WriteF32(0.01f); // diffuse color inverse quant
        w.WriteF32(0.01f); // specular color inverse quant
        w.WriteF32(0.9f); // normal crease parameter
        w.WriteF32(0.5f); // normal update parameter
        w.WriteF32(0.985f); // normal tolerance parameter
        // Skeleton Description.
        w.WriteU32(0); // bone count
        return w.GetBlock(BlockType.MeshDeclaration);
    }

    private static Block GetModelResourceModifierChain(Mesh mesh)
    {
        var w = new BlockWriter();
        w.WriteString(mesh.Name); // modifier chain name, bonded to ModelNodeBlock.ModelResourceName
        w.WriteU32(1); // modifier chain type: 1 = model resource modifier chain
        w.WriteU32(0); // modifier chain attributes: 0 = neither bounding sphere nor
        // Bounding box info present. Padding.
        w.WritePadding();
        w.WriteU32(1); // modifier count in this chain
        w.WriteBlock(GetMeshDeclarationBlock(mesh));
        return w.GetBlock(BlockType.ModifierChain);
    }

    private static Block GetMeshContinuationBlock(Mesh mesh)
    {
        var w = new BlockWriter();
        w.WriteString(mesh.Name); // mesh name
        w.WriteU32(0); // chain index
        // Base Mesh Description.
        w.WriteU32((uint)mesh.Triangles.Count); // base face count
        w.WriteU32((uint)mesh.Positions.Count); // base position count
        w.WriteU32((uint)mesh.Normals.Count); // base normal count
        w.WriteU32(0); // base diffuse color count
        w.WriteU32(0); // base specular color count
        w.WriteU32((uint)mesh.TextureCoordinates.Count); // base texture coordinate count
        // Base Mesh Data.
        foreach (Vector3 position in mesh.Positions)
        {
            w.WriteF32(position.X);
            w.WriteF32(position.Y);
            w.WriteF32(position.Z);
        }
        foreach (Vector3 normal in mesh.Normals)
        {
            w.WriteF32(normal.X);
            w.WriteF32(normal.Y);
            w.WriteF32(normal.Z);
        }
        foreach (Vector2 textureCoordinate in mesh.TextureCoordinates)
        {
            w.WriteF32(textureCoordinate.X);
            w.WriteF32(textureCoordinate.Y);
            w.WriteF32(0);
            w.WriteF32(0);
        }
        // Base Face.
        foreach (Triangle triangle in mesh.Triangles)
        {
            w.WriteU32(0u); // shading id
            foreach (Corner corner in triangle)
            {
                w.WriteU32((uint)corner.Position);
                if (corner.Normal >= 0)
                {
                    w.WriteU32((uint)corner.Normal);
                }
                if (corner.TextureCoordinate >= 0)
                {
                    w.WriteU32((uint)corner.TextureCoordinate);
                }
            }
        }
        return w.GetBlock(BlockType.MeshContinuation);
    }

    private static Block GetLitTextureShaderBlock(Shader shader)
    {
        var w = new BlockWriter();
        w.WriteString(shader.Name); // shader name
        w.WriteU32(1); // Lit texture shader attributes: 1 = Lights enabled
        w.WriteF32(0f); // Alpha Test Reference
        w.WriteU32(0x00000617); // Alpha Test Function: ALWAYS
        w.WriteU32(0x00000605); // Color Blend Function: FB_MULTIPLY
        w.WriteU32(1); // Render pass enabled flags
        w.WriteU32(string.IsNullOrEmpty(shader.Texture) ? 0u : 1u); // Shader channels (active texture count)
        w.WriteU32(0); // Alpha texture channels
        w.WriteString(shader.Material); // Material name
        // Texture information.
        if (!string.IsNullOrEmpty(shader.Texture))
        {
            w.WriteString(shader.Texture); // Texture name
            w.WriteF32(1f); // Texture Intensity
            w.WriteU8(0); // Blend function: 0 - Multiply
            w.WriteU8(1); // Blend Source, 1 - blending constant
            w.WriteF32(1f); // Blend Constant
            w.WriteU8(0x00); // Texture Mod; 0x00: TM_NONE; shader should use texture coordinates of the model
            w.WriteArray(Matrix4.GetIdentityMatrix().ToArray()); // Texture Transform Matrix Element
            w.WriteArray(Matrix4.GetIdentityMatrix().ToArray()); // Texture Wrap Transform Matrix Element
            w.WriteU8(0x03); // Texture Repeat
        }
        return w.GetBlock(BlockType.LitTextureShader);
    }

    private static Block GetMaterialResourceBlock(Material material)
    {
        var w = new BlockWriter();
        w.WriteString(material.Name); // Material resource name
        w.WriteU32(0x0000003F); // Material attributes: use all colors, opacity and reflectivity 0x0000003F
        w.WriteF32(material.Ambient.R); // Ambient red
        w.WriteF32(material.Ambient.G); // Ambient green
        w.WriteF32(material.Ambient.B); // Ambient blue
        w.WriteF32(material.Diffuse.R); // Diffuse red
        w.WriteF32(material.Diffuse.G); // Diffuse green
        w.WriteF32(material.Diffuse.B); // Diffuse blue
        w.WriteF32(material.Specular.R); // Specular red
        w.WriteF32(material.Specular.G); // Specular green
        w.WriteF32(material.Specular.B); // Specular blue
        w.WriteF32(material.Emissive.R); // Emissive red
        w.WriteF32(material.Emissive.G); // Emissive green
        w.WriteF32(material.Emissive.B); // Emissive blue
        w.WriteF32(material.Reflectivity); // Reflectivity
        w.WriteF32(material.Opacity); // Opacity
        return w.GetBlock(BlockType.MaterialResource);
    }

    private static Block GetTextureDeclarationBlock(Texture texture)
    {
        var w = new BlockWriter();
        w.WriteString(texture.Name); // texture name
        // Texture image format.
        w.WriteU32(0); // texture height
        w.WriteU32(0); // texture width
        w.WriteU8(0x0F); // texture image type, 0x0F - color RGBA
        w.WriteU32(1); // continuation image count
        // Continuation image format.
        w.WriteU8((byte)texture.ImageFormat); // compression type, 0x01 - JPEG-24, 0x02 - PNG, 0x03 - JPEG-8, 0x04 - TIFF
        w.WriteU8(0x0F); // texture image channels, 0x0F: alpha, red, green, blue
        w.WriteU16(0x0000); // continuation image attributes, 0x0000 - default attributes
        w.WriteU32((uint)texture.Image.Length); // image data byte count
        return w.GetBlock(BlockType.TextureDeclaration);
    }

    private static Block GetTextureResourceModifierChain(Texture texture)
    {
        var w = new BlockWriter();
        w.WriteString(texture.Name); // modifier chain name
        w.WriteU32(2); // modifier chain type: 2 = texture resource modifier chain
        w.WriteU32(0); // modifier chain attributes: 0 = neither bounding sphere nor
        w.WritePadding(); // modifier chain padding
        w.WriteU32(1); // modifier count in this chain
        w.WriteBlock(GetTextureDeclarationBlock(texture));
        return w.GetBlock(BlockType.ModifierChain);
    }

    private static Block GetTextureContinuationBlock(Texture texture)
    {
        var w = new BlockWriter();
        w.WriteString(texture.Name); // texture name
        w.WriteU32(0); // continuation image index
        // Image data.
        foreach (byte b in texture.Image)
        {
            w.WriteU8(b);
        }
        return w.GetBlock(BlockType.TextureContinuation);
    }

    #endregion PrivateMethods
}
