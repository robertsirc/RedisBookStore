using System.Linq;
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
            await db.HashSetAsync(bookKey, RedisConverter.ToHashEntries(book));
            foreach (var author in book.Authors)
            {
                await db.SetAddAsync(authorKey, author);
            }
            
            return await GetBook(book.Id);
        }

        public async Task<Book> GetBook(string id)
        {
            var db = await _redisProvider.Database();
            var bookKey = new RedisKey(new Book().GetType().Name + ":" + id);
            var authorKey = new RedisKey(new Book().GetType().Name + ":" + id + ":authors");

            var bookHash = db.HashGetAll(bookKey);
            var book = RedisConverter.ConvertFromRedis<Book>(bookHash);
            var authors = db.SetMembers(authorKey);
            
            book.Authors = authors.Select(author => author.ToString()).ToArray();

            return book;
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