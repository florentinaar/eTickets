using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _service.GetAllAsync(n => n.Cinema);

            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetMovieByIdAsync(id);

            return View(item);
        }

       

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            MovieDropdownsVM movieDropdowns = await _service.GetMovieDropdownsAsync();

            var movie = new NewMovieVM()
            {
                Cinemas = new SelectList(movieDropdowns.Cinemas, "Id", "Name"),
                Producers = new SelectList(movieDropdowns.Producers, "Id", "FullName"),
                Actors = new SelectList(movieDropdowns.Actors, "Id", "FullName")
            };


            return View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);

            if(movieDetails == null)
            {
                return View("NotFound");
            }

            MovieDropdownsVM dropdowns = await _service.GetMovieDropdownsAsync();

            var output = new NewMovieVM
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actor_Movies.Select(n => n.ActorId).ToList(),
                Cinemas = new SelectList(dropdowns.Cinemas, "Id", "Name"),
                Producers = new SelectList(dropdowns.Producers, "Id", "FullName"),
                Actors = new SelectList(dropdowns.Actors, "Id", "FullName")
            };

            return View(output);
        }


        #region Post 
        [HttpPost]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString) || n.Description.ToLower().Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", allMovies);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewMovieVM input)
        {
            if (!ModelState.IsValid)
            {
                MovieDropdownsVM movieDropdowns = await _service.GetMovieDropdownsAsync();

                input.Cinemas = new SelectList(movieDropdowns.Cinemas, "Id", "Name");
                input.Producers = new SelectList(movieDropdowns.Producers, "Id", "FullName");
                input.Actors = new SelectList(movieDropdowns.Actors, "Id", "FullName");

                return View(input);
            }

            await _service.UpdateAsync(input);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM input)
        {
            if (!ModelState.IsValid)
            {
                MovieDropdownsVM movieDropdowns = await _service.GetMovieDropdownsAsync();

                input.Cinemas = new SelectList(movieDropdowns.Cinemas, "Id", "Name");
                input.Producers = new SelectList(movieDropdowns.Producers, "Id", "FullName");
                input.Actors = new SelectList(movieDropdowns.Actors, "Id", "FullName");

                return View(input);
            }

            await _service.Create(input);

            return RedirectToAction(nameof(Index));
        }

        #endregion


    }
}
