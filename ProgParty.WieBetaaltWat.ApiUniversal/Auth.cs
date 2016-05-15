using System.Net;

namespace ProgParty.WieBetaaltWat.ApiUniversal
{
    public class Auth
    {
        public static CookieContainer CookieContainer = new CookieContainer();
        public static bool IsLoggedIn { get; set; } = false;
    }
}
