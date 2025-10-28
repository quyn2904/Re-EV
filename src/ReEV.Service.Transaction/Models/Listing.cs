using ReEV.Common;

namespace ReEV.Service.Transaction.Models
{
    public sealed class Listing : Entity
    {
        public string Title { get; set; }
        public string MainImage { get; set; }
        public Guid SellerId { get; set; }
        public User Seller { get; set; } = null!;

        private Listing(Guid listingId, string title, string mainImage, Guid sellerId)
        {
            Id = listingId;
            Title = title;
            MainImage = mainImage;
            SellerId = sellerId;
        }

        public static Listing Create(Guid listingId, string title, string mainImage, Guid sellerId)
        {
            return new Listing(listingId, title, mainImage, sellerId);
        }
    }
}
