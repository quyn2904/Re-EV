namespace ReEV.Service.Auth.Models
{
    public sealed class Session : Entity
    {
        public string Hash { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Session()
        {
        }
    }
}
