using Spire.Pdf.Annotations;
using Spire.Pdf.Graphics;
using Spire.Pdf;
using System.IO;
using Universal3d.Core;

namespace Universal3d.Pdf;
public static class U3DDocumentPdfExtensions
{
    #region PublicMethods

    public static PdfDocument ToPdf(this U3DDocument u3dDoc)
    {
        var pdfDoc = new PdfDocument();
        PdfPageBase page = pdfDoc.Pages.Add(PdfPageSize.A4, new PdfMargins(30.0f), PdfPageRotateAngle.RotateAngle0, PdfPageOrientation.Landscape);
        var rectangle = new System.Drawing.Rectangle(0, 0, (int)(page.Size.Width - 60.0f), (int)(page.Size.Height - 60.0f));

        string u3dTempFileName = Path.GetTempFileName() + ".u3d";
        u3dDoc.Save(u3dTempFileName);

        var annotation = new Pdf3DAnnotation(rectangle, u3dTempFileName)
        {
            Activation = new Pdf3DActivation()
        };
        annotation.Activation.ActivationMode = Pdf3DActivationMode.PageOpen;
        var view = new Pdf3DView
        {
            Background = new Pdf3DBackground(new PdfRGBColor(System.Drawing.Color.White)),
            ExternalName = "Default",
            InternalName = "Default",
            ViewNodeName = "Default",
            RenderMode = new Pdf3DRendermode(Pdf3DRenderStyle.Solid),
            LightingScheme = new Pdf3DLighting()
        };
        view.LightingScheme.Style = Pdf3DLightingStyle.Hard;
        annotation.Views.Add(view);
        page.AnnotationsWidget.Add(annotation);
                
        return pdfDoc;
    }

    #endregion PublicMethods
}
