using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IniLib
{
    public class IniFile
    {
        private readonly IDictionary<string, Section> _dict;

        public IniFile()
        {
            _dict = new Dictionary<string, Section>();
        }

        public Section this[string key]
        {
            get => _dict[key];
            set => _dict[key] = value;
        }

        public IEnumerator<Section> GetEnumerator() => _dict.Select(section => section.Value).GetEnumerator();

        public void Add(string key, out Section v)
        {
            v = new Section(key);
            _dict.Add(key, v);
        }

        public void Add(string key, Section value)
        {
            _dict.Add(key, value);
        }
    }

    public class Section
    {
        private readonly IDictionary<string, string> _dict;
        public string Name { get; }

        public Section(string name)
        {
            Name = name;
            _dict = new Dictionary<string, string>();
        }

        public string this[string key]
        {
            get => _dict[key];
            set => _dict[key] = value;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _dict.GetEnumerator();

        public void Add(string key, string value)
        {
            _dict.Add(key, value);
        }
    }
}