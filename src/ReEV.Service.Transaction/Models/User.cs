namespace ReEV.Service.Transaction.Models
{
    public sealed class User : Entity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Order> OrdersAsBuyer { get; set; } = new List<Order>();
        public ICollection<Order> OrdersAsSeller { get; set; } = new List<Order>();

        private User(Guid id, string fullName, string phoneNumber, string email)
        {
            Id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public static User Create(Guid id, string fullName, string phoneNumber, string email)
        {
            return new User(id, fullName, phoneNumber, email);
        }
    }
}
