using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Pages.LFMAS.LFS
{
    public class IndexModel : PageModel
    {
        private readonly SeriesDataContext _context;

        public IndexModel(SeriesDataContext context)
        {
            _context = context;
        }

        public IList<SerieModel> Series { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Series = await _context.SerieModel.ToListAsync();
            return Page();
        }
    }
}

