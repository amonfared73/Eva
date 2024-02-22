namespace Eva.Core.Domain.BaseModels
{
    public class AesEncryptionConfiguration
    {
        public string Key { get; set; } = string.Empty;
        public string IV { get; set; } = string.Empty;
    }
}
