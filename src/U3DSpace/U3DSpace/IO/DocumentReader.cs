using System.IO;
using U3DSpace.IO.BlockIO;

namespace U3DSpace.IO
{
    public class DocumentReader
    {
        #region PublicMethods

        public static void Read(U3DDocument doc, Stream stream, bool leaveOpen = false)
        {
            using (var reader = new StreamReader(stream, doc.TextEncoding, false, 1024, leaveOpen))
            {
                throw new System.NotImplementedException();
            }
        }

        #endregion PublicMethods

        #region PrivateMethods

        private static Block GetHeaderBlock()
        {
            throw new System.NotImplementedException();
        }

        #endregion PrivateMethods
    }
}