using System;
using System.Collections.Generic;

namespace Universal3d.Core.IO.BlockIO;
/// <summary>Represents data block in bytes</summary>
internal class Block
{
    #region Constructors

    public Block()
    {
        Type = BlockType.Invalid;
        Data = [];
        MetaData = [];
        DataAligning = [];
        MetaDataAligning = [];
    }

    public Block(Block block)
    {
        if (block == null)
        {
            Type = BlockType.Invalid;
            Data = [];
            MetaData = [];
            DataAligning = [];
            MetaDataAligning = [];
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

    public byte[] ToArray()
    {
        var result = new List<byte>();
        result.AddRange(BitConverter.GetBytes((uint)Type));
        result.AddRange(BitConverter.GetBytes((uint)Data.Count));
        result.AddRange(BitConverter.GetBytes((uint)MetaData.Count));
        result.AddRange(Data);
        result.AddRange(DataAligning);
        result.AddRange(MetaData);
        result.AddRange(MetaDataAligning);
        return [.. result];
    }

    private void AlignData()
    {
        byte aligningSize = (byte)(4 - (Data.Count % 4));
        DataAligning = [];
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
        MetaDataAligning = [];
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
