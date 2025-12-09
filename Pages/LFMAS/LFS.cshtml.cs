using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Pages.LFMAS.LFS
{
    public class IndexModel : PageModel
    {
        public IList<Serie> serie { get; set; } = new List<Serie>();
        public IList<SerieModel> Series { get; set; } = new List<SerieModel>();

        private readonly SeriesDataContext _Series;
        private readonly ZAAL_SQL.Data.Context Seriecontext;
        public IndexModel(SeriesDataContext context, ZAAL_SQL.Data.Context SerieContext)
        {
            _Series = context;
            Seriecontext = SerieContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Series = await _Series.SerieModel.ToListAsync();
            serie = await Seriecontext.Serie.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAdd(int ID)
        {
            var Serie = _Series.SerieModel.FirstOrDefault(m => m.ID == ID);
            if (true)
            {
                TempData["Message"] = "added to your serie list!";
                var newSerie = new Serie
                {
                    Titel = Serie?.Titel,
                    Date = Serie?.Date,
                    Genre = Serie?.Genre,
                    Restricting_age = Serie?.Restricting_age,
                    Seasons = Serie?.Seasons,
                    Rating = Serie?.Rating,
                    Poster = Serie?.Poster,
                };


                Seriecontext.Serie.Add(newSerie);
                await Seriecontext.SaveChangesAsync();

                Series = await _Series.SerieModel.ToListAsync();
                serie = await Seriecontext.Serie.ToListAsync();

                return Page();
            }
            else
            {
            }
        }
    }
}

