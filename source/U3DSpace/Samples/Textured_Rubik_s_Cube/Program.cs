using System;
using U3DSpace;

namespace Textured_Rubik_s_Cube
{
    public static class Program
    {
        #region Methods

        public static void Main(string[] args)
        {
            U3DDocument doc = U3DSpace.Samples.Textured_Rubik_s_Cube.GetDocument();

            string u3dFileName = "Textured_Rubik_s_Cube.u3d";
            doc.SaveToFile(u3dFileName);
            Console.WriteLine(@"Successfully saved: {0}\{1}", Environment.CurrentDirectory, u3dFileName);

            string pdfFileName = "Textured_Rubik_s_Cube.pdf";
            doc.SaveToPdfFile(pdfFileName);
            Console.WriteLine(@"Successfully saved: {0}\{1}", Environment.CurrentDirectory, pdfFileName);

            Console.ReadKey();
        }

        #endregion Methods
    }
}