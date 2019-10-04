using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encode
{
    public class CaesarCipher : IEncoder
    {
        protected readonly List<char> Alphabet;
        private Dictionary<char, char> Table { get; } = new Dictionary<char, char>();
        public int AKey { get; set; }

        public CaesarCipher(int a)
        {
            Alphabet = Utility.UaAlphabet;
            AKey = a;
            for (var i = 0; i < Alphabet.Count; i++)
            {
                var index = Index(i);
                Table.Add(Alphabet[i], Alphabet[index]);
            }
        }

        protected virtual int Index(int i) => (i + AKey) % Alphabet.Count;

        public string Encrypt(string text) => Crypt(text, Table);

        public string Decrypt(string text) => Crypt(text, Table.ToDictionary(kp => kp.Value, kp => kp.Key));

        private static string Crypt(string text, Dictionary<char, char> dict)
        {
            var res = new StringBuilder();
            foreach (var t in text)
            {
                res.Append(dict.Keys.Contains(t) ? dict[t] : t);
            }
            return res.ToString();
        }
    }
}