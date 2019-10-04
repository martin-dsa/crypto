using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encode
{
    public class XorCipher : IEncoder
    {
        protected List<char> Alphabet;

        public int Y1 { get; set; }
        public int Y2 { get; set; }
        public int Y3 { get; set; }

        public XorCipher()
        {
            Alphabet = Utility.UaAlphabet;
        }

        public string Encrypt(string text) => Crypt(text, true);

        public string Decrypt(string text) => Crypt(text, false);

        private string Crypt(string text, bool isEnc)
        {
            var key = GetKey(text).ToList();

            var res = new StringBuilder();
            for (var i = 0; i < text.Length; i++)
            {
                res.Append(Alphabet.Contains(text[i]) ?
                    Alphabet[(Alphabet.IndexOf(text[i]) + (isEnc ? key[i] : Alphabet.Count - key[i])) % Alphabet.Count] :
                    text[i]);
            }
            return res.ToString();
        }

        private IEnumerable<int> GetKey(string text)
        {
            var intermediateKey = new List<int> { Y1, Y2, Y3 };
            for (var i = 3; i < text.Length; i++)
            {
                intermediateKey.Add((intermediateKey[i - 1] + intermediateKey[i - 3]) % Alphabet.Count);
            }
            intermediateKey.Add(0);
            for (var i = 0; i < text.Length; i++)
            {
                yield return (intermediateKey[i] + intermediateKey[i + 1]) % Alphabet.Count;
            }
        }
    }
}