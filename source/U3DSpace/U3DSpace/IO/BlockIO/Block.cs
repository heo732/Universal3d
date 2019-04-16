using System.Collections.Generic;

namespace U3DSpace.IO.BlockIO
{
    /// <summary>
    /// Represents data block in bytes.
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

        public Block(BlockType blockType, IEnumerable<byte> data, IEnumerable<byte> metaData)
        {
            Type = blockType;
            Data = new List<byte>(data);
            MetaData = new List<byte>(metaData);
            AlignTo4Bytes();
        }

        #endregion Constructors

        #region Properties

        public List<byte> Data { get; internal set; }

        /// <summary>
        /// Aligning to 4 bytes.
        /// It consists only from zeros.
        /// </summary>
        public List<byte> DataAligning { get; internal set; }

        public List<byte> MetaData { get; internal set; }

        /// <summary>
        /// Aligning to 4 bytes.
        /// It consists only from zeros.
        /// </summary>
        public List<byte> MetaDataAligning { get; internal set; }

        public BlockType Type { get; internal set; }

        #endregion Properties

        #region Methods

        public void AlignTo4Bytes()
        {
            AlignData();
            AlignMetaData();
        }

        private void AlignData()
        {
            byte aligningSize = (byte)(4 - (Data.Count % 4));
            DataAligning = new List<byte>();
            if (aligningSize != 4)
            {
                for (byte i = 0; i < aligningSize; i++)
                {
                    DataAligning.Add(0);
                }
            }
        }

        private void AlignMetaData()
        {
            byte aligningSize = (byte)(4 - (MetaData.Count % 4));
            MetaDataAligning = new List<byte>();
            if (aligningSize != 4)
            {
                for (byte i = 0; i < aligningSize; i++)
                {
                    MetaDataAligning.Add(0);
                }
            }
        }

        #endregion Methods
    }
}