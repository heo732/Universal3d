using System;
using System.Collections.Generic;
using System.Text;

namespace U3DSpace.IO.BlockIO
{
    /// <summary>
    /// Represents byte writer for the block.
    /// Supports only uncompressed values.
    /// </summary>
    public class BlockWriter
    {
        #region Fields

        private Block _block;

        #endregion Fields

        #region Constructors

        public BlockWriter()
        {
            _block = new Block();
        }

        #endregion Constructors

        #region Methods

        public Block GetBlock(BlockType blockType)
        {
            _block.AlignTo4Bytes();
            _block.Type = blockType;
            return new Block(_block);
        }

        public void WriteArray(IEnumerable<float> array)
        {
            foreach (float item in array)
            {
                WriteF32(item);
            }
        }

        public void WriteBlock(Block block)
        {
            WriteU32((uint)block.Type);
            WriteU32((uint)block.Data.Count);
            WriteU32((uint)block.MetaData.Count);
            WriteData(block.Data);
            WriteData(block.DataAligning);
            WriteData(block.MetaData);
            WriteData(block.MetaDataAligning);
        }

        public void WriteData(IEnumerable<byte> data)
        {
            foreach (byte b in data)
            {
                WriteU8(b);
            }
        }

        public void WriteF32(float value)
        {
            WriteData(BitConverter.GetBytes(value));
        }

        public void WriteF64(double value)
        {
            WriteData(BitConverter.GetBytes(value));
        }

        public void WriteI16(short value)
        {
            WriteData(BitConverter.GetBytes(value));
        }

        public void WriteI32(int value)
        {
            WriteData(BitConverter.GetBytes(value));
        }

        public void WriteMetaData(IEnumerable<byte> metaData)
        {
            foreach (byte b in metaData)
            {
                _block.MetaData.Add(b);
            }
        }

        public void WriteMetaF32(float value)
        {
            WriteMetaData(BitConverter.GetBytes(value));
        }

        public void WriteMetaF64(double value)
        {
            WriteMetaData(BitConverter.GetBytes(value));
        }

        public void WriteMetaI16(short value)
        {
            WriteMetaData(BitConverter.GetBytes(value));
        }

        public void WriteMetaI32(int value)
        {
            WriteMetaData(BitConverter.GetBytes(value));
        }

        public void WriteMetaString(string s, Encoding encoding)
        {
            WriteMetaU16((ushort)s.Length);
            WriteMetaData(encoding.GetBytes(s));
        }

        public void WriteMetaString(string s)
        {
            WriteMetaU16((ushort)s.Length);
            WriteMetaData(Encoding.UTF8.GetBytes(s));
        }

        public void WriteMetaU16(ushort value)
        {
            WriteMetaData(BitConverter.GetBytes(value));
        }

        public void WriteMetaU32(uint value)
        {
            WriteMetaData(BitConverter.GetBytes(value));
        }

        public void WriteMetaU64(ulong value)
        {
            WriteMetaData(BitConverter.GetBytes(value));
        }

        public void WriteMetaU8(byte value)
        {
            _block.MetaData.Add(value);
        }

        public void WritePadding()
        {
            byte paddingSize = (byte)(4 - (_block.Data.Count % 4));
            if (paddingSize != 4)
            {
                for (byte i = 0; i < paddingSize; i++)
                {
                    WriteU8(0);
                }
            }
        }

        public void WriteMetaPadding()
        {
            byte paddingSize = (byte)(4 - (_block.MetaData.Count % 4));
            if (paddingSize != 4)
            {
                for (byte i = 0; i < paddingSize; i++)
                {
                    WriteMetaU8(0);
                }
            }
        }

        public void WriteString(string s, Encoding encoding)
        {
            WriteU16((ushort)s.Length);
            WriteData(encoding.GetBytes(s));
        }

        public void WriteString(string s)
        {
            WriteU16((ushort)s.Length);
            WriteData(Encoding.UTF8.GetBytes(s));
        }

        public void WriteU16(ushort value)
        {
            WriteData(BitConverter.GetBytes(value));
        }

        public void WriteU32(uint value)
        {
            WriteData(BitConverter.GetBytes(value));
        }

        public void WriteU64(ulong value)
        {
            WriteData(BitConverter.GetBytes(value));
        }

        public void WriteU8(byte value)
        {
            _block.Data.Add(value);
        }

        #endregion Methods
    }
}