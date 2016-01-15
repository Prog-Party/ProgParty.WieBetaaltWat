using System.Net;

namespace ProgParty.WieBetaaltWat.Api
{
    public class Auth
    {
        public static CookieContainer CookieContainer = new CookieContainer();
        public static bool IsLoggedIn { get; set; } = false;
        public static string Token { get; set; }
    }
}
