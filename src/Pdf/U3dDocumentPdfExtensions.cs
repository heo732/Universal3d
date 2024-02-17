using Spire.Pdf.Annotations;
using Spire.Pdf;
using System.IO;
using Universal3d.Core;
using System.Drawing;

namespace Universal3d.Pdf;
public static class U3dDocumentPdfExtensions
{
    #region PublicMethods

    public static PdfDocument U3dToPdf(this string u3dFilePath) => ToPdfBase(null, u3dFilePath);

    public static PdfDocument ToPdf(this U3dDocument u3dDoc, string u3dTempFilePath = null) => ToPdfBase(u3dDoc, u3dTempFilePath);

    private static PdfDocument ToPdfBase(U3dDocument u3dDoc, string u3dFilePath)
    {
        u3dFilePath ??= Path.GetTempFileName() + ".u3d";
        u3dDoc?.Save(u3dFilePath);

        var pdfDoc = new PdfDocument();
        var page = pdfDoc.Pages.Add(PdfPageSize.A4, new(30f), PdfPageRotateAngle.RotateAngle0, PdfPageOrientation.Landscape);
        var rectangle = new Rectangle(0, 0, (int)(page.Size.Width - 60f), (int)(page.Size.Height - 60f));

        var annotation = new Pdf3DAnnotation(rectangle, u3dFilePath)
        {
            Activation = new()
            {
                ActivationMode = Pdf3DActivationMode.PageOpen,
            },
        };

        var view = new Pdf3DView
        {
            Background = new(new(Color.White)),
            ExternalName = "Default",
            InternalName = "Default",
            ViewNodeName = "Default",
            RenderMode = new(Pdf3DRenderStyle.Solid),
            LightingScheme = new()
            {
                Style = Pdf3DLightingStyle.Hard,
            },
        };

        annotation.Views.Add(view);
        page.AnnotationsWidget.Add(annotation);

        return pdfDoc;
    }

    #endregion PublicMethods
}
