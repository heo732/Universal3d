using System.IO;
using Universal3d.Samples;
using Xunit;

namespace Universal3d.Core.Tests;
public class U3DDocumentTests
{
    [Fact]
    public void Save_ShouldProperlySave_TexturedRubiksCubeSample()
    {
        // Arrange
        var doc = U3DSamples.TexturedRubiksCube;
        using var stream = new MemoryStream();

        // Act
        doc.Save(stream, true);

        // Assert
        Assert.Equal(66588, stream.Length);
    }
}
