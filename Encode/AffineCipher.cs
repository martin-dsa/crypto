namespace Encode
{
    public class AffineCipher : CaesarCipher, IEncoder
    {
        private static int BKey { get; set; }

        public AffineCipher(int a, int b) : base(SetB(a, b))
        {
        }

        private static int SetB(int a, int b)
        {
            BKey = b;
            return a;
        }

        protected override int Index(int i) => (AKey * i + BKey) % Alphabet.Count;
    }
}