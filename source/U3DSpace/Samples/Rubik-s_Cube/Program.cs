﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using U3DSpace;
using U3DSpace.Primitives;
using U3DSpace.Primitives.MaterialPrimitives;
using U3DSpace.Primitives.MeshPrimitives;
using U3DSpace.Primitives.NodePrimitives;
using U3DSpace.Primitives.TexturePrimitives;

namespace Rubik_s_Cube
{
    public class Program
    {
        #region Methods

        public static void Main(string[] args)
        {
            var doc = new U3DDocument();

            AddTextures(doc);
            AddMaterials(doc);
            AddShaders(doc);
            AddMeshes(doc);
            AddNodes(doc);

            doc.SaveToFile("result.u3d");
            doc.SaveToFilePDF("result.pdf");

            System.Console.WriteLine("Successfully!");
        }

        public static byte[] GetImageFromResources(string imageName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(string.Format("Rubik_s_Cube.Images.{0}", imageName)))
            {
                var data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                return data;
            }
        }

        public static void AddTextures(U3DDocument doc)
        {
            doc.TryAddTexture(new Texture("front_texture", ImageFormat.PNG, GetImageFromResources("front.png")));
            //doc.TryAddTexture(new Texture("back", ImageFormat.PNG, GetImageFromResources("back.png")));
            //doc.TryAddTexture(new Texture("left", ImageFormat.PNG, GetImageFromResources("left.png")));
            //doc.TryAddTexture(new Texture("right", ImageFormat.PNG, GetImageFromResources("right.png")));
            //doc.TryAddTexture(new Texture("top", ImageFormat.PNG, GetImageFromResources("top.png")));
            //doc.TryAddTexture(new Texture("bottom", ImageFormat.PNG, GetImageFromResources("bottom.png")));
        }

        public static void AddMaterials(U3DDocument doc)
        {
            doc.TryAddMaterial(new Material("front_material", new Color(0.1), new Color(0.0, 0.0, 1.0), new Color(0.3), new Color(0.1), 0.1f, 1.0f));
            //doc.TryAddMaterial(new Material("back", new Color(0.1), new Color(1.0), new Color(0.3), new Color(0), 0.1f, 1));
            //doc.TryAddMaterial(new Material("left", new Color(0.1), new Color(1.0), new Color(0.3), new Color(0), 0.1f, 1));
            //doc.TryAddMaterial(new Material("right", new Color(0.1), new Color(1.0), new Color(0.3), new Color(0), 0.1f, 1));
            //doc.TryAddMaterial(new Material("top", new Color(0.1), new Color(1.0), new Color(0.3), new Color(0), 0.1f, 1));
            //doc.TryAddMaterial(new Material("bottom", new Color(0.1), new Color(1.0), new Color(0.3), new Color(0), 0.1f, 1));
        }

        public static void AddShaders(U3DDocument doc)
        {
            doc.TryAddShader(new Shader("front_shader", "front_material", "front_texture"));
            //doc.TryAddShader(new Shader("back", "back", "back"));
            //doc.TryAddShader(new Shader("left", "left", "left"));
            //doc.TryAddShader(new Shader("right", "right", "right"));
            //doc.TryAddShader(new Shader("top", "top", "top"));
            //doc.TryAddShader(new Shader("bottom", "bottom", "bottom"));
        }

        public static void AddMeshes(U3DDocument doc)
        {
            doc.TryAddMesh(new Mesh(
                "front_mesh", "front_shader",
                new List<Vector3> { new Vector3(-1), new Vector3(-1, 1, -1), new Vector3(1, 1, -1), new Vector3(1, -1, -1) },
                new List<Vector3> { new Vector3(0, 0, -1) },
                new List<Vector2> { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) },
                new List<Triangle> { new Triangle(new Corner(0, 0, 0), new Corner(1, 0, 1), new Corner(2, 0, 2)) }
                ));
            //doc.TryAddMesh(new Mesh(
            //    "back", "back",
            //    new List<Vector3> { new Vector3(1, -1, 1), new Vector3(1), new Vector3(-1, 1, 1), new Vector3(-1, -1, 1) },
            //    new List<Vector3> { new Vector3(0, 0, 1) },
            //    new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) },
            //    new List<Triangle> { new Triangle(new Corner(0, 0, 0), new Corner(1, 0, 1), new Corner(2, 0, 2)), new Triangle(new Corner(0, 0, 0), new Corner(2, 0, 2), new Corner(3, 0, 3)) }
            //    ));
        }

        public static void AddNodes(U3DDocument doc)
        {
            //string rootNodeName = "Rubik's Cube";
            //doc.TryAddNode(new Node(rootNodeName, null, null, Matrix4.GetIdentityMatrix()));
            doc.TryAddNode(new Node("front", "front_mesh", null, Matrix4.GetIdentityMatrix()));
            //doc.TryAddNode(new Node("back", "back", rootNodeName, Matrix4.GetIdentityMatrix()));
            //doc.TryAddNode(new Node("left", "left", rootNodeName, Matrix4.GetIdentityMatrix()));
            //doc.TryAddNode(new Node("right", "right", rootNodeName, Matrix4.GetIdentityMatrix()));
            //doc.TryAddNode(new Node("top", "top", rootNodeName, Matrix4.GetIdentityMatrix()));
            //doc.TryAddNode(new Node("bottom", "bottom", rootNodeName, Matrix4.GetIdentityMatrix()));
        }

        #endregion Methods
    }
}