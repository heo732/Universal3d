using System.Collections.Generic;

namespace U3DSpace.IO.BlockIO
{
    /// <summary>
    /// Represents data block in bytes.
    /// Data and metadata aligning mean aligning to 4 bytes.
    /// </summary>
    public class Block
    {
        #region Constructors

        public Block()
        {
            Data = new List<byte>();
            MetaData = new List<byte>();
            DataAligning = new List<byte>();
            MetaDataAligning = new List<byte>();
        }

        #endregion Constructors

        #region Properties

        public uint BlockType { get; internal set; }
        public List<byte> Data { get; internal set; }
        public List<byte> DataAligning { get; internal set; }
        public List<byte> MetaData { get; internal set; }
        public List<byte> MetaDataAligning { get; internal set; }

        #endregion Properties
    }
}