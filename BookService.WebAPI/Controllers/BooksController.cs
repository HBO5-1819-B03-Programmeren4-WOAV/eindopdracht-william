using BookService.Lib.DTO;
using BookService.Lib.Models;
using BookService.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace BookService.WebAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerCrudBase<Book, BookRepository>
    {

        public BooksController(BookRepository bookRepository) : base(bookRepository)
        {
        }

        // GET: api/Books
        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            return Ok(await repository.GetAllInclusive());
        }

        // GET: api/Books/Basic
        [HttpGet]
        [Route("Basic")]
        public async Task<IActionResult> GetBookBasic()
        {
            return Ok(await repository.ListBasic());
        }

        // GET: api/Books/Statistics
        [HttpGet]
        [Route("Statistics")]
        public async Task<IActionResult> GetBookStatistics()
        {
            return Ok(await repository.ListStatistics());
        }

        // GET: api/Books/Detail
        [HttpGet]
        [Route("Detail/{id}")]
        public async Task<IActionResult> GetBookDetail(int id)
        {
            return Ok(await repository.GetDetailById(id));
        }

        // GET: api/books/imagebyname/book2.jpg
        [HttpGet]
        [Route("ImageByName/{filename}")]
        public IActionResult ImageByFileName(string filename)
        {
            var image = Path.Combine(Directory.GetCurrentDirectory(),
                             "wwwroot", "images", filename);
            return PhysicalFile(image, "image/jpeg");
        }

        // GET: api/books/imagebyid/6
        [HttpGet]
        [Route("ImageById/{bookid}")]
        public async Task<IActionResult> ImageById(int bookid)
        {
            BookDetail book = await repository.GetDetailById(bookid);
            return ImageByFileName(book.FileName);
        }

        [HttpPost]
        public async override Task<IActionResult> Post([FromBody] Book book)
        {
            book.Author = null;
            book.Publisher = null;

            return await base.Post(book);
        }


        // POST: api/books/image/
        [HttpPost]
        [Route("Image")]
        public async Task<IActionResult> Image(IFormFile formFile)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                             "wwwroot", "images", formFile.FileName);
            if (formFile.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            return Ok(new { count = 1, formFile.Length });
        }

    }
}