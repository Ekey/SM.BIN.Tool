using System;
using System.IO;
using System.Collections.Generic;

namespace SM.Unpacker
{
    class BinUnpack
    {
        static List<BinEntry> m_EntryTable = new List<BinEntry>();

        public static void iDoIt(String m_Archive, String m_DstFolder)
        {
            BinHashList.iLoadProject();

            using (FileStream TBinStream = File.OpenRead(m_Archive))
            {
                Int64 dwCurrentOffset = 0;

                m_EntryTable.Clear();
                do
                {
                    UInt32 dwNameHash = TBinStream.ReadUInt32();
                    UInt32 dwHash = TBinStream.ReadUInt32();
                    UInt32 dwExtHash = TBinStream.ReadUInt32();
                    Int32 dwDecompressedSize = TBinStream.ReadInt32();
                    Int32 dwCompressedSize = TBinStream.ReadInt32();
                    dwCurrentOffset = TBinStream.Position;

                    var TEntry = new BinEntry
                    {
                        dwNameHash = dwNameHash,
                        dwHash = dwHash,
                        dwExtHash = dwExtHash,
                        dwCompressedSize = dwCompressedSize,
                        dwDecompressedSize = dwDecompressedSize,
                        dwOffset = dwCurrentOffset
                    };

                    m_EntryTable.Add(TEntry);

                    dwCurrentOffset += dwCompressedSize;

                    TBinStream.Seek(dwCompressedSize, SeekOrigin.Current);
                }
                while (dwCurrentOffset < TBinStream.Length);

                foreach (var m_Entry in m_EntryTable)
                {
                    String m_FileName = BinHashList.iGetNameFromHashList(m_Entry.dwNameHash).Replace("/", @"\") + "." + BinHashList.iGetNameFromHashList(m_Entry.dwExtHash);
                    String m_FullPath = m_DstFolder + m_FileName;

                    Utils.iSetInfo("[UNPACKING]: " + m_FileName);
                    Utils.iCreateDirectory(m_FullPath);

                    TBinStream.Seek(m_Entry.dwOffset, SeekOrigin.Begin);
                    var lpTemp = TBinStream.ReadBytes(m_Entry.dwCompressedSize);
                    var lpBuffer = LZ4.iDecompress(lpTemp, m_Entry.dwDecompressedSize);

                    File.WriteAllBytes(m_FullPath, lpBuffer);
                }

                TBinStream.Dispose();
            }
        }
    }
}
