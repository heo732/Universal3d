using System.IO;
using System.Reflection;
using U3DSpace;
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

        #endregion Methods
    }
}