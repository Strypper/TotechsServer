using System;

namespace Intranet.AppSettings
{
    public class JwtTokenConfig
    {
        public string Key { get; set; } = String.Empty;
        public string Issuer { get; set; } = String.Empty;
    }
}
