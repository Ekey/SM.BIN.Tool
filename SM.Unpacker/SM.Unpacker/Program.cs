using System;
using System.IO;

namespace SM.Unpacker
{
    class Program
    {
        private static String m_Title = "Sine Mora / EX BIN Unpacker";

        static void Main(String[] args)
        {
            Console.Title = m_Title;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(m_Title);
            Console.WriteLine("(c) 2022 Ekey (h4x0r) / v{0}\n", Utils.iGetApplicationVersion());
            Console.ResetColor();

            if (args.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[Usage]");
                Console.WriteLine("    SM.Unpacker <m_File> <m_Directory>\n");
                Console.WriteLine("    m_File - Source of BIN archive file");
                Console.WriteLine("    m_Directory - Destination directory\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Examples]");
                Console.WriteLine("    SM.Unpacker E:\\Games\\SM\\sinemora_data.bin D:\\Unpacked");
                Console.ResetColor();
                return;
            }

            String m_BinFile = args[0];
            String m_Output = Utils.iCheckArgumentsPath(args[1]);

            if (!File.Exists(m_BinFile))
            {
                Utils.iSetError("[ERROR]: Input BIN file -> " + m_BinFile + " <- does not exist");
                return;
            }

            BinUnpack.iDoIt(m_BinFile, m_Output);
        }
    }
}
