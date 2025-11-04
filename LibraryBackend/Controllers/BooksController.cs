using LibraryBackend.DTOs;
using LibraryBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        
        // Injected service for handling book related operations
        private readonly IBookService _svc;

 
        // Dependency Injection using constructor
        public BookController(IBookService svc)
        {
            _svc = svc;
        }


        // Get API for retrieves all books

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadDto>>> GetAll()
        {
            var result = await _svc.GetAllBooks();
            return Ok(result);
        }



        // Get API for retrieves specific book by it's id

        [HttpGet("{id:int}", Name = "GetBookById")]
        public async Task<ActionResult<BookReadDto>> GetById(int id)
        {
            var book = await _svc.GetBookById(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }



        // Post API for Add a new book

        [HttpPost]
        public async Task<ActionResult<BookReadDto>> Create([FromBody] BookCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var created = await _svc.CreateBook(dto);
                return CreatedAtRoute("GetBookById", new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                // pass duplicate title / ISBN errors to frontend
                return BadRequest(new { message = ex.Message});
            }
        }



        // Put API for Update a book

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updated = await _svc.UpdateBook(id, dto);
                if (!updated)
                    return NotFound();
                
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        // Delete API for delete a specific book by it's id

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _svc.DeleteBook(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}