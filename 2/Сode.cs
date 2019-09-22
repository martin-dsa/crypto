using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2
{
    class Code
    {
        private static readonly List<char> UA_Alphabet = new List<char>
        {
            'а', 'б', 'в', 'г', 'ґ', 'д', 'е', 'є', 'ж', 'з', 'и',
            'і', 'ї', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с',
            'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ю', 'я'
        };
        private Dictionary<char, char> Table { get; } = new Dictionary<char, char>();
        public int A { get; set; }
        public int B { get; set; }

        public Code(int a, int b)
        {
            A = a;
            B = b;
            for (int i = 0; i < UA_Alphabet.Count; i++)
            {
                int index = (A * i + B) % UA_Alphabet.Count;
                Table.Add(UA_Alphabet[i], UA_Alphabet[index]);
            }
        }

        public string Encrypt(string text)
        {
            string res = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (Table.Keys.Contains(text[i]))
                {
                    res += Table[text[i]];
                }
                else
                {
                    res += text[i];
                }
            }
            return res;
        }
    }
}
