using System.IO;
using Universal3d.Samples;
using Xunit;

namespace Universal3d.Core.Tests;
public class U3dDocumentTests
{
    [Fact]
    public void Save_ShouldProperlySave_TexturedRubiksCubeSample()
    {
        // Arrange
        var doc = U3dSamples.TexturedRubiksCube;
        using var stream = new MemoryStream();

        // Act
        doc.Save(stream, true);

        // Assert
        Assert.Equal(66620, stream.Length);
    }

    [Fact]
    public void Load_ShouldProperlyParse_TexturedRubiksCubeSample()
    {
        // Arrange
        var doc = U3dSamples.TexturedRubiksCube;
        using var streamOut = new MemoryStream();
        doc.Save(streamOut, true);
        streamOut.Position = 0;
        using var streamIn = new MemoryStream();

        // Act
        doc = U3dDocument.Load(streamOut, true);
        doc.Save(streamIn, true);

        // Assert
        Assert.Equal(streamOut.Length, streamIn.Length);
    }
}
