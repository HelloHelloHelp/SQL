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
        public IList<MovieModel> Movie { get; set; } = new List<MovieModel>();
        public IList<MovieModel> movies { get; set; } = new List<MovieModel>();

        private readonly MoviesDataContext _context;

        public IndexModel(MoviesDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            movies = await _context.MovieModel.ToListAsync();
            Movie = movies.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAdd()
        {
            if (true)
            {
                TempData["Message"] = $"added to your movie list!";
                var movie = new MovieModel
                {
                    Titel = Request.Form["Title"].ToString(),
                    Date = int.TryParse(Request.Form["Date"], out int date) ? date : null,
                    Genre = Request.Form["Genre"].ToString(),
                    Restricting_age = int.TryParse(Request.Form["Restricting_age"], out int age) ? age : null,
                    Rating = int.TryParse(Request.Form["Rating"], out int rating) ? rating : null,
                    Platform = Request.Form["Platform"].ToString()
                };
                _context.MovieModel.Add(movie);
                await _context.SaveChangesAsync();

            }
            else
            {
            }

          
            movies = await _context.MovieModel.ToListAsync();

            return Page();
        }



    }
}
