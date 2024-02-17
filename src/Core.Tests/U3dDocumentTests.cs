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
        Assert.Equal(66588, stream.Length);
    }
}
