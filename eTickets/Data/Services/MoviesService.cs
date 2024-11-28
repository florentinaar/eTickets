using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Create(NewMovieVM input)
        {
            var movie = new Movie
            {
                Name = input.Name,
                Description = input.Description,
                Price = input.Price,
                ImageURL = input.ImageURL,
                CinemaId = input.CinemaId,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                MovieCategory = input.MovieCategory,
                ProducerId = input.ProducerId
            };

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            foreach (var actor in input.ActorIds)
            {
                var actorMovie = new Actor_Movie
                {
                    ActorId = actor,
                    MovieId = movie.Id
                };

                await _context.AddAsync(actorMovie);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(c => c.Producer)
                .Include(c => c.Actor_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;

        }

        public async Task<MovieDropdownsVM> GetMovieDropdownsAsync()
        {
            var response = new MovieDropdownsVM
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateAsync(NewMovieVM input)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == input.Id);

            if (dbMovie != null)
            {

                dbMovie.Name = input.Name;
                dbMovie.Description = input.Description;
                dbMovie.Price = input.Price;
                dbMovie.ImageURL = input.ImageURL;
                dbMovie.CinemaId = input.CinemaId;
                dbMovie.StartDate = input.StartDate;
                dbMovie.EndDate = input.EndDate;
                dbMovie.MovieCategory = input.MovieCategory;
                dbMovie.ProducerId = input.ProducerId;

                await _context.SaveChangesAsync();

                var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == input.Id).ToList();

                _context.Actors_Movies.RemoveRange(existingActorsDb);
                await _context.SaveChangesAsync();

                foreach (var actor in input.ActorIds)
                {
                    var actorMovie = new Actor_Movie
                    {
                        ActorId = actor,
                        MovieId = input.Id
                    };

                    await _context.Actors_Movies.AddAsync(actorMovie);
                }

                await _context.SaveChangesAsync();
            }


        }
    }
}
