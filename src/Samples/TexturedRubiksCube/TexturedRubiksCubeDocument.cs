﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Universal3d.Core;
using Universal3d.Core.Primitives;
using Universal3d.Core.Primitives.MaterialPrimitives;
using Universal3d.Core.Primitives.MeshPrimitives;
using Universal3d.Core.Primitives.NodePrimitives;
using Universal3d.Core.Primitives.TexturePrimitives;

namespace Universal3d.Samples;
internal static class TexturedRubiksCubeDocument
{
    #region PublicMethods

    public static U3dDocument Build()
    {
        var doc = new U3dDocument();
        AddTextures(doc);
        AddMaterials(doc);
        AddShaders(doc);
        AddMeshes(doc);
        AddNodes(doc);
        return doc;
    }

    #endregion PublicMethods

    #region PrivateMethods

    private static byte[] GetImageFromResources(string imageName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream stream = assembly.GetManifestResourceStream(string.Format("Universal3d.Samples.TexturedRubiksCube.Images.{0}", imageName));
        var data = new byte[stream.Length];
        stream.Read(data, 0, (int)stream.Length);
        return data;
    }

    private static void AddTextures(U3dDocument doc)
    {
        doc.TryAddTexture(new Texture("front_texture", ImageFormat.PNG, GetImageFromResources("front.png")));
        doc.TryAddTexture(new Texture("back_texture", ImageFormat.PNG, GetImageFromResources("back.png")));
        doc.TryAddTexture(new Texture("left_texture", ImageFormat.PNG, GetImageFromResources("left.png")));
        doc.TryAddTexture(new Texture("right_texture", ImageFormat.PNG, GetImageFromResources("right.png")));
        doc.TryAddTexture(new Texture("top_texture", ImageFormat.PNG, GetImageFromResources("top.png")));
        doc.TryAddTexture(new Texture("bottom_texture", ImageFormat.PNG, GetImageFromResources("bottom.png")));
    }

    private static void AddMaterials(U3dDocument doc)
    {
        doc.TryAddMaterial(new Material("default_material", new Color(0.1), new Color(1.0), new Color(0.5), new Color(0.0), 1.0f, 1.0f));
    }

    private static void AddShaders(U3dDocument doc)
    {
        doc.TryAddShader(new Shader("front_shader", "default_material", "front_texture"));
        doc.TryAddShader(new Shader("back_shader", "default_material", "back_texture"));
        doc.TryAddShader(new Shader("left_shader", "default_material", "left_texture"));
        doc.TryAddShader(new Shader("right_shader", "default_material", "right_texture"));
        doc.TryAddShader(new Shader("top_shader", "default_material", "top_texture"));
        doc.TryAddShader(new Shader("bottom_shader", "default_material", "bottom_texture"));
    }

    private static void AddMeshes(U3dDocument doc)
    {
        var triangles = new List<Triangle> { new(new Corner(0, 0, 0), new Corner(1, 0, 1), new Corner(2, 0, 2)), new(new Corner(2, 0, 2), new Corner(3, 0, 3), new Corner(0, 0, 0)) };
        var textureCoordinates = new List<Vector2> { new(0, 0), new(0, 1), new(1, 1), new(1, 0) };
        doc.TryAddMesh(new Mesh(
            "front_mesh", "front_shader",
            [new Vector3(-1, -1, 1), new Vector3(-1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, -1, 1)],
            [new Vector3(0, 0, 1)],
            textureCoordinates,
            triangles
            ));
        doc.TryAddMesh(new Mesh(
            "back_mesh", "back_shader",
            [new Vector3(1, -1, -1), new Vector3(1, 1, -1), new Vector3(-1, 1, -1), new Vector3(-1, -1, -1)],
            [new Vector3(0, 0, -1)],
            textureCoordinates,
            triangles
            ));
        doc.TryAddMesh(new Mesh(
            "left_mesh", "left_shader",
            [new Vector3(-1, -1, -1), new Vector3(-1, 1, -1), new Vector3(-1, 1, 1), new Vector3(-1, -1, 1)],
            [new Vector3(-1, 0, 0)],
            textureCoordinates,
            triangles
            ));
        doc.TryAddMesh(new Mesh(
            "right_mesh", "right_shader",
            [new Vector3(1, -1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, -1), new Vector3(1, -1, -1)],
            [new Vector3(1, 0, 0)],
            textureCoordinates,
            triangles
            ));
        doc.TryAddMesh(new Mesh(
            "top_mesh", "top_shader",
            [new Vector3(-1, 1, 1), new Vector3(-1, 1, -1), new Vector3(1, 1, -1), new Vector3(1, 1, 1)],
            [new Vector3(0, 1, 0)],
            textureCoordinates,
            triangles
            ));
        doc.TryAddMesh(new Mesh(
            "bottom_mesh", "bottom_shader",
            [new Vector3(-1, -1, -1), new Vector3(-1, -1, 1), new Vector3(1, -1, 1), new Vector3(1, -1, -1)],
            [new Vector3(0, -1, 0)],
            textureCoordinates,
            triangles
            ));
    }

    private static void AddNodes(U3dDocument doc)
    {
        string rootNodeName = "Rubik's Cube";
        doc.TryAddNode(new Node(rootNodeName, null, null, Matrix4.GetIdentityMatrix()));
        doc.TryAddNode(new Node("front", "front_mesh", rootNodeName, Matrix4.GetIdentityMatrix()));
        doc.TryAddNode(new Node("back", "back_mesh", rootNodeName, Matrix4.GetIdentityMatrix()));
        doc.TryAddNode(new Node("left", "left_mesh", rootNodeName, Matrix4.GetIdentityMatrix()));
        doc.TryAddNode(new Node("right", "right_mesh", rootNodeName, Matrix4.GetIdentityMatrix()));
        doc.TryAddNode(new Node("top", "top_mesh", rootNodeName, Matrix4.GetIdentityMatrix()));
        doc.TryAddNode(new Node("bottom", "bottom_mesh", rootNodeName, Matrix4.GetIdentityMatrix()));
    }

    #endregion PrivateMethods
}
