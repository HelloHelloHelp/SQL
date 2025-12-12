using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Pages.Series
{
    public class EditModel : PageModel
    {
        private readonly Context _context;
        private readonly HttpClient _http = new HttpClient();

        public EditModel(Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Serie Serie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Serie = await _context.Serie.FirstOrDefaultAsync(m => m.ID == id);

            if (Serie == null)
                return NotFound();

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
            Serie.PosterUrl = omdb.OmdbPosterUrl;
            Serie.Title = title;
            Serie.Year = omdb.Year;
            Serie.Genre = omdb.Genre;
            Serie.TotalSeasons = omdb.TotalSeasons;
            Serie.ImdbRating = omdb.ImdbRating;

            _context.Attach(Serie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerieExists(Serie.ID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }

        private bool SerieExists(int id)
        {
            return _context.Serie.Any(e => e.ID == id);
        }
    }
}
