namespace Rms.Application.Common
{
    public class RmsSystemConfig
    {
        public OAuthSettings OAuthSettings { get; set; }
    }

    public class OAuthSettings
    {
        public string GoogleCallbackUrl { get; set; }
    }

}
