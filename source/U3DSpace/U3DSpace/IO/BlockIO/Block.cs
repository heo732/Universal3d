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
            Type = BlockType.Invalid;
            Data = new List<byte>();
            MetaData = new List<byte>();
            DataAligning = new List<byte>();
            MetaDataAligning = new List<byte>();
        }

        public Block(Block block)
        {
            if (block == null)
            {
                Type = BlockType.Invalid;
                Data = new List<byte>();
                MetaData = new List<byte>();
                DataAligning = new List<byte>();
                MetaDataAligning = new List<byte>();
            }
            else
            {
                Type = block.Type;
                Data = new List<byte>(block.Data);
                MetaData = new List<byte>(block.MetaData);
                DataAligning = new List<byte>(block.DataAligning);
                MetaDataAligning = new List<byte>(block.MetaDataAligning);
            }
        }

        #endregion Constructors

        #region Properties

        public List<byte> Data { get; internal set; }
        public List<byte> DataAligning { get; internal set; }
        public List<byte> MetaData { get; internal set; }
        public List<byte> MetaDataAligning { get; internal set; }
        public BlockType Type { get; internal set; }

        #endregion Properties
    }
}