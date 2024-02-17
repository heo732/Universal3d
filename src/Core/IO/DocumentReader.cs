using System.IO;
using Universal3d.Core.IO.BlockIO;

namespace Universal3d.Core.IO;
internal class DocumentReader
{
    #region PublicMethods

    public static U3dDocument Read(Stream stream, bool leaveOpen)
    {
        var doc = new U3dDocument();
        using var reader = new StreamReader(stream, doc.TextEncoding, false, 1024, leaveOpen);
        throw new System.NotImplementedException();
    }

    #endregion PublicMethods

    #region PrivateMethods

    private static Block GetHeaderBlock()
    {
        throw new System.NotImplementedException();
    }

    #endregion PrivateMethods
}
