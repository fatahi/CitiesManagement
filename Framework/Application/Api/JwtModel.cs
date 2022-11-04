namespace Framework.Application.Api
{
    public class JwtModel
    {
        public string Key { get; set; }
        public int TokenTimeout { get; set; }
        public int RefreshTokenTimeout { get; set; }
    }
}
