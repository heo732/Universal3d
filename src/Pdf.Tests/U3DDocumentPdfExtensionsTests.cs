using Spire.Pdf;
using System.IO;
using Universal3d.Samples;
using Xunit;

namespace Universal3d.Pdf.Tests;
public class U3DDocumentPdfExtensionsTests
{
    [Fact]
    public void ToPdf_ShouldCreateCorrectPdf_FromTexturedRubiksCubeSample()
    {
        // Arrange
        var u3dDoc = U3DSamples.TexturedRubiksCube;
        using var stream = new MemoryStream();

        // Act
        using var pdfDoc = u3dDoc.ToPdf();
        pdfDoc.SaveToStream(stream, FileFormat.PDF);

        // Assert
        Assert.Equal(11094, stream.Length);
    }
}
