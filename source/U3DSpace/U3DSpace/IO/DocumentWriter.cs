using Spire.Pdf;
using Spire.Pdf.Annotations;
using Spire.Pdf.Graphics;
using System.IO;
using U3DSpace.IO.BlockIO;

namespace U3DSpace.IO
{
    public static class DocumentWriter
    {
        #region PublicMethods

        public static void SavePDF(Stream pdfDocStream, U3DDocument u3dDoc)
        {
            var pdfDoc = new PdfDocument();
            PdfPageBase page = pdfDoc.Pages.Add(PdfPageSize.A4, new PdfMargins(30.0f), PdfPageRotateAngle.RotateAngle0, PdfPageOrientation.Landscape);
            var rectangle = new System.Drawing.Rectangle(0, 0, (int)(page.Size.Width - 60.0f), (int)(page.Size.Height - 60.0f));

            string u3dTempFileName = Path.GetTempFileName();
            using (var u3dDocStream = new FileStream(u3dTempFileName, FileMode.Create))
            {
                Save(u3dDocStream, u3dDoc);
            }
            Pdf3DAnnotation annotation = new Pdf3DAnnotation(rectangle, u3dTempFileName);
            File.Delete(u3dTempFileName);

            annotation.Activation = new Pdf3DActivation();
            annotation.Activation.ActivationMode = Pdf3DActivationMode.PageOpen;
            Pdf3DView View = new Pdf3DView();
            View.Background = new Pdf3DBackground(new PdfRGBColor(System.Drawing.Color.White));
            View.ViewNodeName = "DefaultView";
            View.RenderMode = new Pdf3DRendermode(Pdf3DRenderStyle.Solid);
            View.InternalName = "Default";
            View.LightingScheme = new Pdf3DLighting();
            View.LightingScheme.Style = Pdf3DLightingStyle.Day;
            annotation.Views.Add(View);
            page.AnnotationsWidget.Add(annotation);

            pdfDoc.SaveToStream(pdfDocStream, FileFormat.PDF);
        }

        public static void Save(Stream stream, U3DDocument doc)
        {
            using (var writer = new BinaryWriter(stream, doc.TextEncoding, true))
            {
                writer.Write(GetHeaderBlock(doc).ToArray());
            }
        }

        #endregion PublicMethods

        #region PrivateMethods

        private static Block GetHeaderBlock(U3DDocument doc)
        {
            var w = new BlockWriter();
            w.WriteI32(0x00000000); // version
            w.WriteU32(0x00000004); // profile identifier (0x00000004 - No compression mode)
            w.WriteU32(36); // declaration size
            w.WriteU64(732); // file size
            w.WriteU32(106); // character encoding: 106 = UTF-8
            //Meta data.
            w.WriteMetaU32(1); // Key/Value Pair Count
            w.WriteMetaU32(0); // Key/Value Pair Attributes; 0x00000000 - indicates the Value is formatted as a String
            w.WriteMetaString("{Created_by", doc.TextEncoding); // Key String
            w.WriteMetaString("GLTFtoU3D_converter}", doc.TextEncoding); // Value String
            return w.GetBlock(BlockType.Header);
        }

        #endregion PrivateMethods
    }
}