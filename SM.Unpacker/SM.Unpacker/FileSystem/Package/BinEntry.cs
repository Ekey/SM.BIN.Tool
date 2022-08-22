using System;

namespace SM.Unpacker
{
    class BinEntry
    {
        public UInt32 dwNameHash { get; set; }
        public UInt32 dwHash { get; set; }
        public UInt32 dwExtHash { get; set; }
        public Int32 dwDecompressedSize { get; set; }
        public Int32 dwCompressedSize { get; set; }
        public Int64 dwOffset { get; set; }
    }
}
