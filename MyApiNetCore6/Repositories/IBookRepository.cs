using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories
{
    public interface IBookRepository
    {
        public Task<List<BookModel>> GetAllBooksAsync();
        public Task<BookModel> GetBooksAsync(int id);
        public Task<int> AddBookdAsync(BookModel model);
        public Task UpdateBookdAsync(int id, BookModel model);
        public Task DeleteBookAsync(int id);

    }
}   

