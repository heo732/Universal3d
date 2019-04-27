using System.Collections.Generic;
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
    public static class Document
    {
        #region PublicMethods

        public static U3DDocument GetDocument()
        {
            var doc = new U3DDocument();
            AddTextures(doc);
            AddMaterials(doc);
            AddShaders(doc);
            AddMeshes(doc);
            AddNodes(doc);
            return doc;
        }

        public static void Save(Stream stream)
        {
            GetDocument().Save(stream);
        }

        public static void SaveToFile(string filePath)
        {
            GetDocument().SaveToFile(filePath);
        }

        public static void SavePDF(Stream stream)
        {
            GetDocument().SavePDF(stream);
        }

        public static void SaveToPdfFile(string filePath)
        {
            GetDocument().SaveToPdfFile(filePath);
        }

        #endregion PublicMethods

        #region PrivateMethods

        private static void Main(string[] args)
        {
            var doc = GetDocument();
            doc.SaveToFile("result.u3d");
            doc.SaveToPdfFile("result.pdf");
            System.Console.WriteLine("Successfully!");
        }

        private static byte[] GetImageFromResources(string imageName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(string.Format("Rubik_s_Cube.Images.{0}", imageName)))
            {
                var data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                return data;
            }
        }

        private static void AddTextures(U3DDocument doc)
        {
            doc.TryAddTexture(new Texture("front_texture", ImageFormat.PNG, GetImageFromResources("front.png")));
            doc.TryAddTexture(new Texture("back_texture", ImageFormat.PNG, GetImageFromResources("back.png")));
            doc.TryAddTexture(new Texture("left_texture", ImageFormat.PNG, GetImageFromResources("left.png")));
            doc.TryAddTexture(new Texture("right_texture", ImageFormat.PNG, GetImageFromResources("right.png")));
            doc.TryAddTexture(new Texture("top_texture", ImageFormat.PNG, GetImageFromResources("top.png")));
            doc.TryAddTexture(new Texture("bottom_texture", ImageFormat.PNG, GetImageFromResources("bottom.png")));
        }

        private static void AddMaterials(U3DDocument doc)
        {
            doc.TryAddMaterial(new Material("default_material", new Color(0.1), new Color(1.0), new Color(0.5), new Color(0.0), 1.0f, 1.0f));
        }

        private static void AddShaders(U3DDocument doc)
        {
            doc.TryAddShader(new Shader("front_shader", "default_material", "front_texture"));
            doc.TryAddShader(new Shader("back_shader", "default_material", "back_texture"));
            doc.TryAddShader(new Shader("left_shader", "default_material", "left_texture"));
            doc.TryAddShader(new Shader("right_shader", "default_material", "right_texture"));
            doc.TryAddShader(new Shader("top_shader", "default_material", "top_texture"));
            doc.TryAddShader(new Shader("bottom_shader", "default_material", "bottom_texture"));
        }

        private static void AddMeshes(U3DDocument doc)
        {
            var triangles = new List<Triangle> { new Triangle(new Corner(0, 0, 0), new Corner(1, 0, 1), new Corner(2, 0, 2)), new Triangle(new Corner(2, 0, 2), new Corner(3, 0, 3), new Corner(0, 0, 0)) };
            var textureCoordinates = new List<Vector2> { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };
            doc.TryAddMesh(new Mesh(
                "front_mesh", "front_shader",
                new List<Vector3> { new Vector3(-1, -1, 1), new Vector3(-1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, -1, 1) },
                new List<Vector3> { new Vector3(0, 0, 1) },
                textureCoordinates,
                triangles
                ));
            doc.TryAddMesh(new Mesh(
                "back_mesh", "back_shader",
                new List<Vector3> { new Vector3(1, -1, -1), new Vector3(1, 1, -1), new Vector3(-1, 1, -1), new Vector3(-1, -1, -1) },
                new List<Vector3> { new Vector3(0, 0, -1) },
                textureCoordinates,
                triangles
                ));
            doc.TryAddMesh(new Mesh(
                "left_mesh", "left_shader",
                new List<Vector3> { new Vector3(-1, -1, -1), new Vector3(-1, 1, -1), new Vector3(-1, 1, 1), new Vector3(-1, -1, 1) },
                new List<Vector3> { new Vector3(-1, 0, 0) },
                textureCoordinates,
                triangles
                ));
            doc.TryAddMesh(new Mesh(
                "right_mesh", "right_shader",
                new List<Vector3> { new Vector3(1, -1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, -1), new Vector3(1, -1, -1) },
                new List<Vector3> { new Vector3(1, 0, 0) },
                textureCoordinates,
                triangles
                ));
            doc.TryAddMesh(new Mesh(
                "top_mesh", "top_shader",
                new List<Vector3> { new Vector3(-1, 1, 1), new Vector3(-1, 1, -1), new Vector3(1, 1, -1), new Vector3(1, 1, 1) },
                new List<Vector3> { new Vector3(0, 1, 0) },
                textureCoordinates,
                triangles
                ));
            doc.TryAddMesh(new Mesh(
                "bottom_mesh", "bottom_shader",
                new List<Vector3> { new Vector3(-1, -1, -1), new Vector3(-1, -1, 1), new Vector3(1, -1, 1), new Vector3(1, -1, -1) },
                new List<Vector3> { new Vector3(0, -1, 0) },
                textureCoordinates,
                triangles
                ));
        }

        private static void AddNodes(U3DDocument doc)
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
}