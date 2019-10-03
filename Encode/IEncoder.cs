namespace Encode
{
    public interface IEncoder
    {
        string Decrypt(string text);

        string Encrypt(string text);
    }
}