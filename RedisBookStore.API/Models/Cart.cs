using System.Collections.Generic;

namespace RedisBookStore.API.Models
{
    public class Cart
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}