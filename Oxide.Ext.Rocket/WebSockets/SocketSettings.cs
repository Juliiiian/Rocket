namespace Oxide.Ext.Rocket.WebSockets
{
    public class SocketSettings
    {
        public string ApiToken;

        public string Url;

        public SocketSettings(string ApiToken_arg, string Url_arg)
        {
            ApiToken = ApiToken_arg;
            Url = Url_arg;
        }
    }
}