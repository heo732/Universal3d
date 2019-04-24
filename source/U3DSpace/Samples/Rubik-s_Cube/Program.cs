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
    public class Program
    {
        #region Methods

        public static void Main(string[] args)
        {
            var doc = new U3DDocument();

            AddTextures(doc);
            doc.TryAddMaterial(new Material("default", new Color(0.1), new Color(1.0), new Color(0.3), new Color(0), 0.1f, 1));
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
            doc.TryAddTexture(new Texture("front", ImageFormat.PNG, GetImageFromResources("front.png")));
            doc.TryAddTexture(new Texture("back", ImageFormat.PNG, GetImageFromResources("back.png")));
            doc.TryAddTexture(new Texture("left", ImageFormat.PNG, GetImageFromResources("left.png")));
            doc.TryAddTexture(new Texture("right", ImageFormat.PNG, GetImageFromResources("right.png")));
            doc.TryAddTexture(new Texture("top", ImageFormat.PNG, GetImageFromResources("top.png")));
            doc.TryAddTexture(new Texture("bottom", ImageFormat.PNG, GetImageFromResources("bottom.png")));
        }

        public static void AddShaders(U3DDocument doc)
        {
            doc.TryAddShader(new Shader("front", "default", "front"));
            doc.TryAddShader(new Shader("back", "default", "back"));
            doc.TryAddShader(new Shader("left", "default", "left"));
            doc.TryAddShader(new Shader("right", "default", "right"));
            doc.TryAddShader(new Shader("top", "default", "top"));
            doc.TryAddShader(new Shader("bottom", "default", "bottom"));
        }

        public static void AddMeshes(U3DDocument doc)
        {
            doc.TryAddMesh(new Mesh(
                "front", "front",
                new List<Vector3> { new Vector3(-1), new Vector3(-1, 1, -1), new Vector3(1, 1, -1), new Vector3(1, -1, -1) },
                new List<Vector3> { new Vector3(0, 0, -1) },
                new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) },
                new List<Triangle> { new Triangle(new Corner(0, 0, 0), new Corner(1, 0, 1), new Corner(2, 0, 2)), new Triangle(new Corner(0, 0, 0), new Corner(2, 0, 2), new Corner(3, 0, 3)) }
                ));
            doc.TryAddMesh(new Mesh(
                "back", "back",
                new List<Vector3> { new Vector3(1, -1, 1), new Vector3(1), new Vector3(-1, 1, 1), new Vector3(-1, -1, 1) },
                new List<Vector3> { new Vector3(0, 0, 1) },
                new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) },
                new List<Triangle> { new Triangle(new Corner(0, 0, 0), new Corner(1, 0, 1), new Corner(2, 0, 2)), new Triangle(new Corner(0, 0, 0), new Corner(2, 0, 2), new Corner(3, 0, 3)) }
                ));
        }

        public static void AddNodes(U3DDocument doc)
        {
            string rootNodeName = "Rubik's Cube";
            doc.TryAddNode(new Node(rootNodeName, null, null, Matrix4.GetIdentityMatrix()));
            doc.TryAddNode(new Node("front", "front", rootNodeName, Matrix4.GetIdentityMatrix()));
            doc.TryAddNode(new Node("back", "back", rootNodeName, Matrix4.GetIdentityMatrix()));
            //doc.TryAddNode(new Node("left", "left", rootNodeName, Matrix4.GetIdentityMatrix()));
            //doc.TryAddNode(new Node("right", "right", rootNodeName, Matrix4.GetIdentityMatrix()));
            //doc.TryAddNode(new Node("top", "top", rootNodeName, Matrix4.GetIdentityMatrix()));
            //doc.TryAddNode(new Node("bottom", "bottom", rootNodeName, Matrix4.GetIdentityMatrix()));
        }

        #endregion Methods
    }
}