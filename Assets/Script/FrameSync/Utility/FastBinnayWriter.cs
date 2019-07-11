﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
namespace THEDARKKNIGHT.SyncSystem.FrameSync.Utility
{
    public sealed class FastBinnayWriter
    {
        readonly UTF8Encoding _encoding = new UTF8Encoding();
        private Stream _stream;
        private byte[] _ioBuffer;
        private int _ioIndex;
        private int _position;

        public FastBinnayWriter()
        {
            _ioBuffer = new byte[256];
        }

        public void Init(Stream s)
        {
            _stream = s;
            _ioIndex = 0;
            _position = 0;
        }

        private void DemandSpace(int required)
        {
            if ((_ioBuffer.Length - _ioIndex) < required)
            {
                Flush(); // try emptying the buffer
                if ((_ioBuffer.Length - _ioIndex) >= required)
                {
                    return;
                }
            }
        }

        public void Flush()
        {
            if (_ioIndex != 0)
            {
                _stream.Write(_ioBuffer, 0, _ioIndex);
                _ioIndex = 0;
            }
        }

        internal uint Zig(int value)
        {
            return (uint)((value << 1) ^ (value >> 31));
        }

        public void Write(int value)
        {
            Write(Zig(value));
        }

        public void Write(uint value)
        {
            DemandSpace(5);
            int count = 0;
            do
            {
                _ioBuffer[_ioIndex++] = (byte)((value & 0x7F) | 0x80);
                count++;
            } while ((value >>= 7) != 0);
            _ioBuffer[_ioIndex - 1] &= 0x7F;
            _position += count;
        }


        internal ulong Zig(long value)
        {
            return (ulong)((value << 1) ^ (value >> 63));
        }

        public void Write(long value)
        {
            Write(Zig(value));
        }
        private void Write(ulong value)
        {
            DemandSpace(10);
            int count = 0;
            do
            {
                _ioBuffer[_ioIndex++] = (byte)((value & 0x7F) | 0x80);
                count++;
            } while ((value >>= 7) != 0);
            _ioBuffer[_ioIndex - 1] &= 0x7F;
            _position += count;
        }

        public void Write(string value)
        {
            int len = value.Length;
            if (len == 0)
            {
                Write(0);
                return; // just a header
            }
            int predicted = _encoding.GetByteCount(value);
            Write((uint)predicted);
            DemandSpace(predicted);
            int actual = _encoding.GetBytes(value, 0, value.Length, _ioBuffer, _ioIndex);
            _ioIndex += actual;
            _position += actual;
        }

        public void Write(bool value)
        {
            Write(value ? (uint)1 : (uint)0);
        }

        /// <summary>
        /// create the serializble the class to the memorystream
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializable"></param>
        public void Write(byte[] value)
        {
            int len = value.Length;
            if (len == 0) {
                Write(0);
                return; // just a header
            }
            Write((uint)len);//write the head length
            DemandSpace(len);
            int dataLenght = len;
            do
            {
                if (dataLenght <= (_ioBuffer.Length - _ioIndex)) {
                    Buffer.BlockCopy(value, 0, _ioBuffer, _ioIndex, dataLenght);
                    dataLenght = 0;
                    _position =+ dataLenght;
                }
                else {
                    Buffer.BlockCopy(value, 0, _ioBuffer, _ioIndex, _ioBuffer.Length);
                    dataLenght = dataLenght - (_ioBuffer.Length - _ioIndex);
                    _position = +_ioBuffer.Length;
                }
                Flush();
            } while (dataLenght > 0);
        }

        public void Reset() {
            _ioIndex = 0;
            _position = 0;
        }

        public void clear()
        {
            _stream = null;
        }
    }
}