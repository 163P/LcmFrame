using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniLib
{
    internal static class StringExt
    {
        internal static bool IsEmptyOrWhiteSpace(this string value) =>
            value.Length == 0 || value.All(char.IsWhiteSpace);
    }

    public class ReadParser
    {
        public static async Task<IniFile> ReadFileAsync(string file)
        {
            return await ReadFileAsync(file, Encoding.Default);
        }

        public static async Task<IniFile> ReadFileAsync(string file, Encoding encoding)
        {
            var result = new IniFile();
            Section section = null;

            using (var sr = new StreamReader(file, encoding))
            {
                await Task.Run(async () =>
                {
                    string line;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        if (line.IsEmptyOrWhiteSpace() || line[0] == ';')
                            continue;
                        if (line[0] == '[')
                        {
                            result.Add(line.Substring(1, line.Length - 2), out section);
                            continue;
                        }
                        AddToSection(ref section, line);
                    }
                });
            }

            return result;
        }

        internal static void AddToSection(ref Section section, string line)
        {
            var pos = line.IndexOf('=');
            section.Add(line.Substring(0, pos), line.Substring(pos + 1, line.Length - pos - 1));
        }
    }

    public class WriteParser
    {
        public static async Task WriteFileAsync(IniFile data, string file, bool overwrite = false)
        {
            if (File.Exists(file))
                if (overwrite)
                    File.Delete(file);
                else
                    throw new Exception($"{file} is exist!");

            await Task.Run(async () =>
            {
                using (var sw = new StreamWriter(file))
                {
                    foreach (var section in data)
                    {
                        await sw.WriteLineAsync($"[{section.Name}]");
                        foreach (var kv in section)
                        {
                            await sw.WriteLineAsync($"{kv.Key}={kv.Value}");
                        }
                    }
                }
            });
        }
    }
}
