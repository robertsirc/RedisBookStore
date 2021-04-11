namespace RedisBookStore.API.Models
{
    public class BookRating
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public int Rating { get; set; }
    }
}