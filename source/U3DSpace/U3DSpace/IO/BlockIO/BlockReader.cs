using System;
using System.Text;

namespace U3DSpace.IO.BlockIO
{
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

        public bool TryToReadString(Encoding encoding, out string value)
        {
            if (DataSise < 2)
            {
                value = string.Empty;
                return false;
            }
            else
            {
                throw new NotImplementedException();
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
}