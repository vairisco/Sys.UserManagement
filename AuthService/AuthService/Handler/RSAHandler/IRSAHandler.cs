namespace WalletService.API.Handler.RSAHandler
{
    public interface IRSAHandler
    {
        string Encrypt(string text);
        string Decrypt(string encrypted);
    }
}
