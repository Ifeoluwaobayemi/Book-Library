using AutoMapper;
using Library.API.Data.Entities;
using Library.API.DTOs;
using Library.API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookRepository _repo;
        private readonly IMapper _mapper;

        public BookController(ILogger<BookController> logger, IBookRepository repo, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;

        }

        [Authorize(Roles = "admin")]
        [HttpPost("add")]
        public IActionResult AddBook([FromBody] AddBookDto model)
        {
            if (ModelState.IsValid)
            {
                var addBook = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    Description = model.Description,
                    NumOfPages = model.NumOfPages
                };
                if (_repo.Add(addBook))
                {
                    return Ok("Book added successfully!");
                }
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var booksToGet = _repo.GetAll();

            var result = booksToGet.Select(x => new Book
            {
                Id = x.Id,
                NumOfPages = x.NumOfPages,
                Author = x.Author,
                Title = x.Title,
                Description = x.Description
            });
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("single/{id}")]
        public IActionResult GetBook(string id)
        {
            if (ModelState.IsValid)
            {
                var aBook = _repo.GetBookById(id);

                if (aBook == null)
                {
                    var result = new ReturnBookDto
                    {
                        Id = aBook.Id,
                        Author = aBook.Author,
                        Title = aBook.Title,
                        CreatedAt = DateTime.Now,
                        PublishedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,

                    };
                    return Ok(result);
                }
            }

            return NotFound($"Book with id: {id} is not found!");
        }

        [AllowAnonymous]
        [HttpPut("update/{id}")]
        public IActionResult EditBook(string id, [FromBody] UpdateBookDto model)
        {
            if (ModelState.IsValid)
            {
                var book = _repo.GetBookById(id);
                if (book != null)
                {
                    book.NumOfPages = model.NumOfPages;
                    book.Title = model.Title;
                    book.Description = model.Description;
                    book.Author = model.Author;

                    if (_repo.Update(book))
                    {
                        return Ok("Book Updated successfully!");
                    }

                    return BadRequest("Update failed");
                }
            }
            return BadRequest($"Failed! Book with id: {id} not updated.");
        }

        [AllowAnonymous]
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteBook(string id)
        {
            if (ModelState.IsValid)
            {
                var book = _repo.GetBookById(id);
                if (book != null)
                {
                    if (_repo.Delete(book))
                    {
                        return Ok("Book deleted successfully!");
                    }
                    return BadRequest("Failed to delete!");
                }
            }

            return BadRequest("$\"Failed! Book with id: {id} not updated.");
        }

        [AllowAnonymous]
        [HttpPatch("single/{id}")]
        public IActionResult EditBookAuthor(string id, [FromBody] AddBookDto model)
        {
            if (ModelState.IsValid)
            {
                var book = _repo.GetBookById(id);
                if (book != null)
                {
                    book.Author = model.Author;

                    if (_repo.Update(book))
                    {
                        return Ok("Book Updated successfully!");
                    }

                    return BadRequest("Update failed");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
