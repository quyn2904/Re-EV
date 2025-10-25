namespace ReEV.Service.Auth.Models
{
    public sealed class User : Entity
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string HashedPassword { get; set; }
        public string? AvatarUrl { get; set; } = null;
        public float Balance { get; set; } = 0;
        public UserRole Role { get; set; } = UserRole.MEMBER;
        public UserStatus Status { get; set; } = UserStatus.UNVERIFIED;
        public ICollection<Session> Sessions { get; set; } = new List<Session>();

        private User(string email, string phoneNumber, string fullName, string hashedPassword)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            FullName = fullName;
            HashedPassword = hashedPassword;
        }

        public static User Create(string email, string phoneNumber, string fullName, string hashedPassword)
        {
            return new User(email, phoneNumber, fullName, hashedPassword);
        }
    }
}
