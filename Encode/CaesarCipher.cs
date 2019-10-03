using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encode
{
    public class CaesarCipher : IEncoder
    {
        protected List<char> Alphabet;
        protected Dictionary<char, char> Table { get; } = new Dictionary<char, char>();
        public int AKey { get; set; }

        public CaesarCipher(int a)
        {
            Alphabet = Utility.UAAlphabet;
            AKey = a;
            for (int i = 0; i < Alphabet.Count; i++)
            {
                int index = Index(i);
                Table.Add(Alphabet[i], Alphabet[index]);
            }
        }

        protected virtual int Index(int i) => (i + AKey) % Alphabet.Count;

        public string Encrypt(string text) => Crypt(text, Table);

        public string Decrypt(string text) => Crypt(text, Table.ToDictionary(kp => kp.Value, kp => kp.Key));

        private string Crypt(string text, Dictionary<char, char> dict)
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                res.Append(dict.Keys.Contains(text[i]) ? dict[text[i]] : text[i]);
            }
            return res.ToString();
        }
    }
}