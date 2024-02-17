using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Universal3d.Core.Enums;

namespace Universal3d.Core.IO.BlockIO;
/// <summary>
/// Represents byte reader for the block.
/// Supports only uncompressed values.
/// </summary>
internal class BlockReader
{
    #region Fields

    private readonly Block _block;

    #endregion Fields

    #region Properties

    public Block Block => _block;

    public int DataSize
    {
        get
        {
            return _block.Data.Count;
        }
    }

    public bool IsDataEmpty
    {
        get
        {
            return _block.Data.Count == 0;
        }
    }

    public bool IsMetaDataEmpty
    {
        get
        {
            return _block.MetaData.Count == 0;
        }
    }

    public int MetaDataSize
    {
        get
        {
            return _block.MetaData.Count;
        }
    }

    #endregion Properties

    #region Constructors

    private BlockReader(Block block)
    {
        _block = block;
    }

    #endregion Constructors

    #region Methods

    public static BlockReader Parse(BinaryReader reader)
    {
        var block = new Block();

        block.Type = (BlockType)reader.ReadUInt32();

        var dataLen = (int)reader.ReadUInt32();
        var metadataLen = (int)reader.ReadUInt32();

        block.Data = [.. reader.ReadBytes(dataLen)];
        
        var dataAlignLen = 4 - (dataLen % 4);
        if (dataAlignLen == 4) dataAlignLen = 0;
        block.DataAligning = [.. reader.ReadBytes(dataAlignLen)];
        
        block.MetaData = [.. reader.ReadBytes(metadataLen)];

        var metadataAlignLen = 4 - (metadataLen % 4);
        if (metadataAlignLen == 4) metadataAlignLen = 0;
        block.MetaDataAligning = [.. reader.ReadBytes(metadataAlignLen)];

        return new(block);
    }

    public IEnumerable<U3dMetaItem> ReadMeta(Encoding encoding)
    {
        if (!TryReadMetaU32(out var count))
            yield break;

        for (int i = 0; i < count; i++)
        {
            var item = new U3dMetaItem();
            
            if (TryReadMetaU32(out var attributes)) item.Attributes = (U3dMetaItemAttributes)attributes;
            if (TryReadMetaString(encoding, out var key)) item.Key = key;
            
            if (item.Attributes.HasFlag(U3dMetaItemAttributes.String))
            {
                if (TryReadMetaString(encoding, out var stringValue)) item.StringValue = stringValue;
            }
            else
            {
                if (!TryReadMetaU32(out var binarySize))
                    continue;

                for (int j = 0; j < binarySize; j++)
                    if (TryReadMetaU8(out var binaryChunk)) item.BinaryValue.Add(binaryChunk);
            }

            yield return item;
        }
    }

    public void ReadPadding()
    {
        for (int i = 0; i < _block.DataAligning.Count; i++)
            TryReadU8(out _);

        _block.DataAligning.Clear();
    }

    public void ReadMetaPadding()
    {
        for (int i = 0; i < _block.MetaDataAligning.Count; i++)
            TryReadU8(out _);

        _block.MetaDataAligning.Clear();
    }

    public BlockReader ReadBlock()
    {
        using var stream = new MemoryStream([.. _block.Data]);
        using var reader = new BinaryReader(stream);
        var block = Parse(reader);
        _block.Data.RemoveRange(0, block.Block.FullSize);
        return block;
    }

    public float[] ReadArray(int size) => Enumerable.Repeat(0, size).Select(x =>
    {
        TryReadF32(out var v);
        return v;
    }).ToArray();

    public bool TryReadF32(out float value)
    {
        if (DataSize < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToSingle([.. _block.Data.GetRange(0, 4)], 0);
            _block.Data.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryReadF64(out double value)
    {
        if (DataSize < 8)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToDouble([.. _block.Data.GetRange(0, 8)], 0);
            _block.Data.RemoveRange(0, 8);
            return true;
        }
    }

    public bool TryReadI16(out short value)
    {
        if (DataSize < 2)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToInt16([.. _block.Data.GetRange(0, 2)], 0);
            _block.Data.RemoveRange(0, 2);
            return true;
        }
    }

    public bool TryReadI32(out int value)
    {
        if (DataSize < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToInt32([.. _block.Data.GetRange(0, 4)], 0);
            _block.Data.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryReadMetaF32(out float value)
    {
        if (MetaDataSize < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToSingle([.. _block.MetaData.GetRange(0, 4)], 0);
            _block.MetaData.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryReadMetaF64(out double value)
    {
        if (MetaDataSize < 8)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToDouble([.. _block.MetaData.GetRange(0, 8)], 0);
            _block.MetaData.RemoveRange(0, 8);
            return true;
        }
    }

    public bool TryReadMetaI16(out short value)
    {
        if (MetaDataSize < 2)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToInt16([.. _block.MetaData.GetRange(0, 2)], 0);
            _block.MetaData.RemoveRange(0, 2);
            return true;
        }
    }

    public bool TryReadMetaI32(out int value)
    {
        if (MetaDataSize < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToInt32([.. _block.MetaData.GetRange(0, 4)], 0);
            _block.MetaData.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryReadMetaString(Encoding encoding, out string value)
    {
        if (TryReadMetaU16(out ushort length))
        {
            if (length > 0)
            {
                if (MetaDataSize >= length)
                {
                    value = encoding.GetString([.. _block.MetaData.GetRange(0, length)]);
                    _block.MetaData.RemoveRange(0, length);
                    return true;
                }
                else
                {
                    value = string.Empty;
                    return false;
                }
            }
            else
            {
                value = string.Empty;
                return true;
            }
        }
        else
        {
            value = string.Empty;
            return false;
        }
    }

    public bool TryReadMetaU16(out ushort value)
    {
        if (MetaDataSize < 2)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt16([.. _block.MetaData.GetRange(0, 2)], 0);
            _block.MetaData.RemoveRange(0, 2);
            return true;
        }
    }

    public bool TryReadMetaU32(out uint value)
    {
        if (MetaDataSize < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt32([.. _block.MetaData.GetRange(0, 4)], 0);
            _block.MetaData.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryReadMetaU64(out ulong value)
    {
        if (MetaDataSize < 8)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt64([.. _block.MetaData.GetRange(0, 8)], 0);
            _block.MetaData.RemoveRange(0, 8);
            return true;
        }
    }

    public bool TryReadMetaU8(out byte value)
    {
        if (IsMetaDataEmpty)
        {
            value = 0;
            return false;
        }
        else
        {
            value = _block.MetaData[0];
            _block.MetaData.RemoveAt(0);
            return true;
        }
    }

    public bool TryReadString(Encoding encoding, out string value)
    {
        if (TryReadU16(out ushort length))
        {
            if (length > 0)
            {
                if (DataSize >= length)
                {
                    value = encoding.GetString([.. _block.Data.GetRange(0, length)]);
                    _block.Data.RemoveRange(0, length);
                    return true;
                }
                else
                {
                    value = string.Empty;
                    return false;
                }
            }
            else
            {
                value = string.Empty;
                return true;
            }
        }
        else
        {
            value = string.Empty;
            return false;
        }
    }

    public bool TryReadU16(out ushort value)
    {
        if (DataSize < 2)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt16([.. _block.Data.GetRange(0, 2)], 0);
            _block.Data.RemoveRange(0, 2);
            return true;
        }
    }

    public bool TryReadU32(out uint value)
    {
        if (DataSize < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt32([.. _block.Data.GetRange(0, 4)], 0);
            _block.Data.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryReadU64(out ulong value)
    {
        if (DataSize < 8)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt64([.. _block.Data.GetRange(0, 8)], 0);
            _block.Data.RemoveRange(0, 8);
            return true;
        }
    }

    public bool TryReadU8(out byte value)
    {
        if (IsDataEmpty)
        {
            value = 0;
            return false;
        }
        else
        {
            value = _block.Data[0];
            _block.Data.RemoveAt(0);
            return true;
        }
    }

    #endregion Methods
}
