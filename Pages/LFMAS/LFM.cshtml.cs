using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Pages.LFMAS.LFM
{
    public class IndexModel : PageModel
    {
        private readonly MoviesDataContext _context;

        public IndexModel(MoviesDataContext context)
        {
            _context = context;
        }

        public IList<MovieModel> movies { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            movies = await _context.MovieModel.ToListAsync();
            return Page();
        }
    }
}
