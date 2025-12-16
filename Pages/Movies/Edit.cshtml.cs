using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly ZAAL_SQLContext _context;
        private readonly HttpClient _http = new HttpClient();

        public EditModel(ZAAL_SQLContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);

            if (movie == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            string title = movie.Title ?? "";
            string url = $"https://www.omdbapi.com/?apikey=3f124dfe&t={Uri.EscapeDataString(title)}";

            var json = await _http.GetStringAsync(url);
            var omdb = JsonConvert.DeserializeObject<Serie>(json);

            if (omdb == null || omdb.OmdbPosterUrl == "N/A")
            {
                ModelState.AddModelError("", "No information found for this title.");
                return Page();
            }

            byte[]? posterBytes = null;

            if (!string.IsNullOrEmpty(omdb.OmdbPosterUrl))
            {
                try
                {
                    posterBytes = await _http.GetByteArrayAsync(omdb.OmdbPosterUrl);
                }
                catch { }
            }

            movie.Poster = posterBytes;
            movie.PosterUrl = omdb.OmdbPosterUrl;
            movie.Director = omdb.Director;
            movie.ImdbID = omdb.ImdbID;
            movie.Genre = omdb.Genre;
            movie.Year = omdb.Year;
            movie.Plot = omdb.Plot;
            movie.Rated = omdb.Rated;
            movie.Language = omdb.Language;
            movie.Runtime = omdb.Runtime;
            _context.Attach(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerieExists(movie.ID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }

        private bool SerieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
