using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using AutoMapper;
using BookStoreApp.API.Models.Book;
using BookStoreApp.API.Static;
using BookStoreApp.API.Models.Author;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<AuthorsController> logger;

        public BooksController(BookStoreDbContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            try
            {
                var bookDtos = await _context.Books
                    .Include(q => q.Author)
                    .ProjectTo<BookReadOnlyDto>(mapper.ConfigurationProvider)
                    .ToListAsync();

            return Ok(bookDtos);
            }
            catch (Exception ex )
            {
                logger.LogError(ex, $"Error performin GET in {nameof(GetBooks)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {

            try
            {
                var book = await _context.Books
                               .Include(q => q.Author)
                               .ProjectTo<BookDetailsDto>(mapper.ConfigurationProvider)
                               .FirstOrDefaultAsync( q => q.Id == id);
                if (book == null)
                {
                    logger.LogWarning($"Book not found: {nameof(GetBook)} - ID: {id}");
                    return NotFound();
                }

                return book;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error performin GET in {nameof(GetBook)}");
                return StatusCode(500, Messages.Error500Message);
            }

        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookUpdateDto)
        {

            try
            {
                if (id != bookUpdateDto.Id)
                {
                    logger.LogWarning($"Book not found: {nameof(PutBook)} - ID: {id}");
                    return BadRequest();
                }

                var book = await _context.Books.FindAsync( id);
                if (book == null)
                {
                    logger.LogWarning($"{nameof(Book)} not found: {nameof(PutBook)} - ID: {id}");
                    return NotFound();
                }

                mapper.Map(bookUpdateDto, book);
                _context.Entry(bookUpdateDto).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!await BookExistsAsync(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        logger.LogError(ex, $"Error performing PUT in {nameof(PutBook)}");
                        return StatusCode(500, Messages.Error500Message);
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error performin PUT in {nameof(PutBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
           
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookCreateDto)
        {

            try
            {
                var book = mapper.Map<Book>(bookCreateDto);
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBook), new {id =  book.Id}, book);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error performin POST in {nameof(PostBook)}", bookCreateDto);
                return StatusCode(500, Messages.Error500Message); ;
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {

            try
            {
                if (_context.Books == null)
                {
                    return NotFound();
                }
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error performin DELETE in {nameof(DeleteBook)}");
                return StatusCode(500, Messages.Error500Message); 
            }
          
        }

        private async  Task<bool> BookExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(e => e.Id == id);
        }
    }
}
