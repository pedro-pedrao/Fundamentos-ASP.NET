namespace Blog
{
    public static class Configuration
    {
        //Token
        public static string JWTKey  = "doAAqjh89E2muyx0y3vQswmGIs0QVhIX";

        public static string ApiKeyName = "api_key";

        public static string ApiKey = "curso_api_IlTevUM/z0ey3VzSV/cnWG==";
        public static SmtpConfiguration Smtp = new ();

        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        
    }
}