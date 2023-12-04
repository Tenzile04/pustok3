using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustokk.DAL;
using Pustokk.Models;
using Pustokk.Services.Interfaces;

namespace Pustokk.Areas.Manage.Controllers
{
    [Area("manage")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
           _genreService = genreService;
        }
        public async Task<IActionResult> Index()
        {
            List<Genre> Genres =await  _genreService.GetAllAsync();
            return View(Genres);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (!ModelState.IsValid) return View();

           await _genreService.CreateAsync(genre);
            
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id == null) return NotFound();
            Genre existGenre =await _genreService.GetByIdAsync(id);
            if (existGenre == null) return NotFound();

            return View(existGenre);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Genre genre)
        {
            if (!ModelState.IsValid) return View();
            await _genreService.UpdateAsync(genre);
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.Delete(id);
            return Ok();
        }

        //[HttpPost]
        //public IActionResult Delete(Genre genre)
        //{

        //    Genre existGenre = _context.Genres.FirstOrDefault(g => g.Id == genre.Id);

        //    if (existGenre == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Genres.Remove(existGenre);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}
    }
}
