using LibraryBackend.DTOs;

namespace LibraryBackend.Services
{
    // IBookService interface for BookService
    public interface IBookService
    {
        // retrieves all book records / return the result as BookReadDto
        Task<IEnumerable<BookReadDto>> GetAllBooks();

        // retrieves a single book by it's id / return result as BookReadDto or null
        Task<BookReadDto?> GetBookById(int id);

        // create a new book record using the data from BookCreateDto / return result as BookReadDto
        Task<BookReadDto> CreateBook(BookCreateDto dto);

        // update a book by Id with the data from / return boolean value
        Task<bool> UpdateBook(int id, BookUpdateDto dto);
        
        // delete a book by Id / return boolean value
        Task<bool> DeleteBook(int id);
    }
}