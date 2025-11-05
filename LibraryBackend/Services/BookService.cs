using LibraryBackend.Data;
using LibraryBackend.DTOs;
using LibraryBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Services
{
    // Implement IBookService interface to BookService for hadle CRUD Operations
    public class BookService : IBookService
    {

        // Injected database context for DB operations
        private readonly LibraryContext _ctx;

        // Dependency Injection using constructor
        public BookService(LibraryContext ctx)
        {
            _ctx = ctx;
        }

        // Convert Book entity to BookReadDto by ToReadDto
        private static BookReadDto ToReadDto(Book b) =>
        new BookReadDto
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            ISBN = b.ISBN,
            Category = b.Category
        };

        //Get all books from tha database 
        public async Task<IEnumerable<BookReadDto>> GetAllBooks()
        {
            var books = await _ctx.Books.AsNoTracking().ToListAsync();
            return books.Select(ToReadDto);
        }

        
        // Get a single book by ID
        public async Task<BookReadDto?> GetBookById(int id)
        {
            var book = await _ctx.Books.FindAsync(id);
            return book is null ? null : ToReadDto(book);
        }


        // Create a new Book in the database
        public async Task<BookReadDto> CreateBook(BookCreateDto dto)
        {

            // Check duplicate title
            if (await _ctx.Books.AnyAsync(b => b.Title == dto.Title))
                throw new InvalidOperationException("A book with this title already exist !");

            // Check for duplicate ISBN
            if (!string.IsNullOrEmpty(dto.ISBN) && await _ctx.Books.AnyAsync(b => b.ISBN == dto.ISBN))
                throw new InvalidOperationException("A book with this ISBN already exists.");    

            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                Category = dto.Category
            };

            _ctx.Books.Add(book);
            await _ctx.SaveChangesAsync();

            return ToReadDto(book);
        }


        // Update a book using by book ID
        public async Task<bool> UpdateBook(int id, BookUpdateDto dto)
        {
            var book = await _ctx.Books.FindAsync(id);
            if (book == null)
                return false;

            // Check if another book has the same Title
            if (await _ctx.Books.AnyAsync(b => b.Title == dto.Title && b.Id != id))
                throw new InvalidOperationException("Another book with this title already exists.");

            // Check if another book has the same ISBN
            if (!string.IsNullOrEmpty(dto.ISBN) && await _ctx.Books.AnyAsync(b => b.ISBN == dto.ISBN && b.Id != id))
                throw new InvalidOperationException("Another book with this ISBN already exists.");
                
    
            book.Title = dto.Title;
            book.Author = dto.Author;
            book.ISBN = dto.ISBN;
            book.Category = dto.Category;

            _ctx.Books.Update(book);
            await _ctx.SaveChangesAsync();
            return true;
        }
        

        // Delete a book by ID
        public async Task<bool> DeleteBook(int id)
        {
            var book = await _ctx.Books.FindAsync(id);
            if (book == null)
                return false;

            _ctx.Books.Remove(book);
            await _ctx.SaveChangesAsync();
            return true;    
        }

    }
}