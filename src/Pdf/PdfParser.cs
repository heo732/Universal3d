using Spire.Pdf;
using Spire.Pdf.Annotations;
using System.IO;
using System.Linq;
using Universal3d.Core;

namespace Universal3d.Pdf;
public static class PdfParser
{
    #region PublicMethods

    public static U3dDocument[] ExtractU3dDocuments(PdfDocument pdfDoc)
    {
        var streams = ExtractU3dStreams(pdfDoc);
        var docs = streams.Select(s => U3dDocument.Load(s, false)).ToArray();
        return docs;
    }

    public static MemoryStream[] ExtractU3dStreams(PdfDocument pdfDoc)
    {
        var annotations = pdfDoc.Pages.OfType<PdfPageBase>().SelectMany(p => p.AnnotationsWidget.OfType<Pdf3DAnnotation>()).ToList();
        var streams = annotations.Select(a => new MemoryStream(a._3DData)).ToArray();
        return streams;
    }

    #endregion PublicMethods
}
