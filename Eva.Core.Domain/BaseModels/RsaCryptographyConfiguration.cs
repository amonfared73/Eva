namespace Eva.Core.Domain.BaseModels
{
    public class RsaCryptographyConfiguration
    {
        public int KeySize { get; set; }
        public string PrivateKey { get; set; } = string.Empty;
        public string PublicKey { get; set; } = string.Empty;
    }
}
