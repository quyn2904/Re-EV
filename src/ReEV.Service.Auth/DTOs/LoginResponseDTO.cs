namespace ReEV.Service.Auth.DTOs
{
    public class LoginResponseDTO
    {
        public TokenResponseDTO TokenResponse { get; set; }
        public UserDTO User { get; set; }
    }
}
