namespace ReEV.Service.Auth.Models
{
    public sealed class RefreshToken
    {

        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public RefreshToken()
        {
        }
    }
}
