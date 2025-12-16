using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Pages.Series
{   
    public class CreateModel : PageModel
    {
        private readonly Context _context;
        private readonly HttpClient _http = new HttpClient();

        public CreateModel(Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Serie Serie { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            
            string title = Serie.Title ?? "";

       
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

            
            Serie.Poster = posterBytes;
            Serie.Language = omdb.Language;
            Serie.PosterUrl = omdb.OmdbPosterUrl;
            Serie.Genre = omdb.Genre;
            Serie.Year = omdb.Year;
            Serie.Plot = omdb.Plot;
            Serie.Runtime = omdb.Runtime;
            Serie.Director = omdb.Director;
            Serie.ImdbID = omdb.ImdbID;
            Serie.TotalSeasons = omdb.TotalSeasons;
            Serie.ImdbRating = omdb.ImdbRating;
            Serie.Rated = omdb.Rated;

            _context.Serie.Add(Serie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
