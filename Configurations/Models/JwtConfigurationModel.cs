namespace Mappy.Configurations.Models;

public class JwtConfigurationModel
{
    public string SecretKey { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int ExpiryTimeInSeconds { get; set; }
}