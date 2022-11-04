namespace Challenge.Application.Contracts.UserRefreshToken
{
    public class AuthenticateViewModel
    {
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
