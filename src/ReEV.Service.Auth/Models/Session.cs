namespace ReEV.Service.Auth.Models
{
    public sealed class Session : Entity
    {
        public string Hash { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        private Session(string hash, Guid userId)
        {
            Hash = hash;
            UserId = userId;
        }

        public static Session Create(string hash, Guid userId)
        {
            return new Session(hash, userId);
        }
    }
}
