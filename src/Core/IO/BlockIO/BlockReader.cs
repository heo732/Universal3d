using System;
using System.Text;

namespace Universal3d.Core.IO.BlockIO;

/// <summary>
/// Represents byte reader for the block.
/// Supports only uncompressed values.
/// </summary>
public class BlockReader
{
    #region Fields

    private Block _block;

    #endregion Fields

    #region Constructors

    public BlockReader(Block block)
    {
        _block = new Block(block);
    }

    #endregion Constructors

    #region Properties

    public int DataSise
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

    public int MetaDataSise
    {
        get
        {
            return _block.MetaData.Count;
        }
    }

    #endregion Properties

    #region Methods

    public bool TryToReadF32(out float value)
    {
        if (DataSise < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToSingle(_block.Data.GetRange(0, 4).ToArray(), 0);
            _block.Data.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryToReadF64(out double value)
    {
        if (DataSise < 8)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToDouble(_block.Data.GetRange(0, 8).ToArray(), 0);
            _block.Data.RemoveRange(0, 8);
            return true;
        }
    }

    public bool TryToReadI16(out short value)
    {
        if (DataSise < 2)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToInt16(_block.Data.GetRange(0, 2).ToArray(), 0);
            _block.Data.RemoveRange(0, 2);
            return true;
        }
    }

    public bool TryToReadI32(out int value)
    {
        if (DataSise < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToInt32(_block.Data.GetRange(0, 4).ToArray(), 0);
            _block.Data.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryToReadMetaF32(out float value)
    {
        if (MetaDataSise < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToSingle(_block.MetaData.GetRange(0, 4).ToArray(), 0);
            _block.MetaData.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryToReadMetaF64(out double value)
    {
        if (MetaDataSise < 8)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToDouble(_block.MetaData.GetRange(0, 8).ToArray(), 0);
            _block.MetaData.RemoveRange(0, 8);
            return true;
        }
    }

    public bool TryToReadMetaI16(out short value)
    {
        if (MetaDataSise < 2)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToInt16(_block.MetaData.GetRange(0, 2).ToArray(), 0);
            _block.MetaData.RemoveRange(0, 2);
            return true;
        }
    }

    public bool TryToReadMetaI32(out int value)
    {
        if (MetaDataSise < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToInt32(_block.MetaData.GetRange(0, 4).ToArray(), 0);
            _block.MetaData.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryToReadMetaString(Encoding encoding, out string value)
    {
        if (TryToReadMetaU16(out ushort length))
        {
            if (length > 0)
            {
                if (MetaDataSise >= length)
                {
                    value = encoding.GetString(_block.MetaData.GetRange(0, length).ToArray());
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

    public bool TryToReadMetaU16(out ushort value)
    {
        if (MetaDataSise < 2)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt16(_block.MetaData.GetRange(0, 2).ToArray(), 0);
            _block.MetaData.RemoveRange(0, 2);
            return true;
        }
    }

    public bool TryToReadMetaU32(out uint value)
    {
        if (MetaDataSise < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt32(_block.MetaData.GetRange(0, 4).ToArray(), 0);
            _block.MetaData.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryToReadMetaU64(out ulong value)
    {
        if (MetaDataSise < 8)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt64(_block.MetaData.GetRange(0, 8).ToArray(), 0);
            _block.MetaData.RemoveRange(0, 8);
            return true;
        }
    }

    public bool TryToReadMetaU8(out byte value)
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

    public bool TryToReadString(Encoding encoding, out string value)
    {
        if (TryToReadU16(out ushort length))
        {
            if (length > 0)
            {
                if (DataSise >= length)
                {
                    value = encoding.GetString(_block.Data.GetRange(0, length).ToArray());
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

    public bool TryToReadU16(out ushort value)
    {
        if (DataSise < 2)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt16(_block.Data.GetRange(0, 2).ToArray(), 0);
            _block.Data.RemoveRange(0, 2);
            return true;
        }
    }

    public bool TryToReadU32(out uint value)
    {
        if (DataSise < 4)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt32(_block.Data.GetRange(0, 4).ToArray(), 0);
            _block.Data.RemoveRange(0, 4);
            return true;
        }
    }

    public bool TryToReadU64(out ulong value)
    {
        if (DataSise < 8)
        {
            value = 0;
            return false;
        }
        else
        {
            value = BitConverter.ToUInt64(_block.Data.GetRange(0, 8).ToArray(), 0);
            _block.Data.RemoveRange(0, 8);
            return true;
        }
    }

    public bool TryToReadU8(out byte value)
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