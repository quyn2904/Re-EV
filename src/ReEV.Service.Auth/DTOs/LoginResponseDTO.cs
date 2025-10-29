namespace ReEV.Service.Auth.DTOs
{
    public class LoginResponseDTO
    {
        public string JwtToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public UserDTO User { get; set; }
    }
}
