using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly ZAAL_SQLContext _context;
        private readonly HttpClient _http = new HttpClient();

        public CreateModel(ZAAL_SQLContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie movie { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();


            string title = movie.Title ?? "";


            string url = $"https://www.omdbapi.com/?apikey=3f124dfe&t={Uri.EscapeDataString(title)}";

            var json = await _http.GetStringAsync(url);
            var omdb = JsonConvert.DeserializeObject<Movie>(json);

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
            movie.Genre = omdb.Genre;
            movie.Year = omdb.Year;
            movie.Plot = omdb.Plot;
            movie.Title = title;

            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
