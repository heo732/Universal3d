using System.IO;
using U3DSpace.IO.BlockIO;

namespace U3DSpace.IO
{
    public class DocumentReader
    {
        #region PublicMethods

        public static void Read(Stream stream, U3DDocument doc)
        {
            using (var reader = new StreamReader(stream))
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