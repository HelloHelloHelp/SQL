using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;


namespace ZAAL_SQL.Pages.LFMAS.LFM
{
    public class IndexModel : PageModel
    {
        public IList<Movie> Movie { get; set; } = new List<Movie>();
        public IList<MovieModel> movies { get; set; } = new List<MovieModel>();

        private readonly MoviesDataContext _context;
        private readonly ZAAL_SQL.Data.ZAAL_SQLContext Moviecontext;
        public IndexModel(MoviesDataContext context, ZAAL_SQL.Data.ZAAL_SQLContext movieContext)
        {
            _context = context;
            Moviecontext = movieContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            movies = await _context.MovieModel.ToListAsync();
            Movie = await Moviecontext.Movie.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAdd(int ID)
        {
            var movie = _context.MovieModel.FirstOrDefault(m => m.ID == ID);
            if (true)
            {
                TempData["Message"] = "added to your movie list!";
                var newMovie = new Movie
                {
                    Titel = movie?.Titel,  
                    Date = movie?.Date,
                    Genre = movie?.Genre,
                    Restricting_age = movie?.Restricting_age,
                    Rating = movie?.Rating,
                    Platform = movie?.Platform
                };
                

                Moviecontext.Movie.Add(newMovie);
                await Moviecontext.SaveChangesAsync(); 

                movies = await _context.MovieModel.ToListAsync();
                Movie = await Moviecontext.Movie.ToListAsync(); 

                return Page();
            }
            else
            {
            }
        }
    }
}
