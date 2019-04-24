using System.Collections.Generic;
using System.IO;
using System.Reflection;
using U3DSpace;
using U3DSpace.Primitives;
using U3DSpace.Primitives.MaterialPrimitives;
using U3DSpace.Primitives.MeshPrimitives;
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
            AddMaterial(doc);
            AddShaders(doc);
            AddMeshes(doc);
        }

        public static byte[] GetImageFromResources(string imageName)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            Stream stream = assembly.GetManifestResourceStream(string.Format(@"Resources\{0}", imageName));
            var data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            return data;
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

        public static void AddMaterial(U3DDocument doc)
        {
            doc.TryAddMaterial(new Material("default", new Color(0.1f), new Color(1.0f), new Color(0.3f), new Color(0.0f), 0.1f, 1.0f));
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
                new List<Vector3> { new Vector3(1.0, 1.0, 1.0) }
                ));
        }

        #endregion Methods
    }
}