using System.Threading.Tasks;
using RedisBookStore.API.Helpers;
using RedisBookStore.API.Models;
using RedisBookStore.API.Providers;
using StackExchange.Redis;

namespace RedisBookStore.API.Services
{
    public class BookService
    {
        private readonly RedisProvider _redisProvider;

        public BookService(RedisProvider redisProvider)
        {
            _redisProvider = redisProvider;
        }

        public async Task<Book> CreateBook(Book book)
        {
            var db = await _redisProvider.Database();
            var bookKey = new RedisKey(book.GetType().Name + ":" + book.Id);
            var authorKey = new RedisKey(book.GetType().Name + ":" + book.Id + ":authors");
            db.HashSet(bookKey, RedisConverter.ToHashEntries(book));
            foreach (var author in book.Authors)
            {
                db.SetAdd(authorKey, author);
            }
            
            return await Task.Run(() => new Book());
        }

        public async Task<Book>  GetBook(string id)
        {
            var bookKey = new RedisKey(new Book().GetType().Name + ":" + id);
            
            
            //TODO add in the actual return not a mock
            
            return await Task.Run(() => new Book());
        }

        public async Task<Book[]> CreateBooks(Book[] books)
        {
            foreach (var book in books)
            {
                await CreateBook(book);
            }
            return new[] {await Task.Run(() => new Book())};
        }

        public void UpdateBook(Book book)
        {
            
        }
    }
}