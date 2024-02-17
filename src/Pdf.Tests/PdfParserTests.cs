using System.IO;
using Universal3d.Samples;
using Xunit;

namespace Universal3d.Pdf.Tests;
public class PdfParserTests
{
    [Fact]
    public void ExtractU3dStreams_ShouldGetCorrectU3d_FromTexturedRubiksCubeSample()
    {
        // Arrange
        var u3dDoc = U3dSamples.TexturedRubiksCube;
        using var u3dStreamOut = new MemoryStream();
        u3dDoc.Save(u3dStreamOut);

        using var pdfDoc = u3dDoc.ToPdf();
        using var pdfStream = new MemoryStream();
        pdfDoc.SaveToStream(pdfStream);
        pdfDoc.LoadFromStream(pdfStream);

        // Act
        var u3dStreams = PdfParser.ExtractU3dStreams(pdfDoc);

        // Assert
        Assert.Single(u3dStreams);
        Assert.Equal(u3dStreamOut.ToArray(), u3dStreams[0].ToArray());
    }
}
