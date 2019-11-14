namespace HotelLocker.DAL.Entities
{
    public class HotelAdmin : User
    {
        public HotelAdmin() : base() { }
        public virtual Hotel Hotel { get; set; }
    }
}
