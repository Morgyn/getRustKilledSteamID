using System;
using Microsoft.Win32;
using System.IO;
using System.Text;

namespace getRustKilledSteamID
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            RegistryKey rustKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 252490", false);
            if (rustKey == null)
            {
                Console.WriteLine("Couldn't open registry");
                return;
            }
            
            String path = (String)rustKey.GetValue("InstallLocation");
            Console.WriteLine("Rust Path: {0}", path);

            using (StreamReader sr = File.OpenText(path + "/output_log.txt"))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.StartsWith("You died: killed by") )
                        Console.WriteLine(s);
                }
            }

            Console.ReadLine();
        }
    }
}
