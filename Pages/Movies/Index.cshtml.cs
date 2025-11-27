using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;
using static System.Net.WebRequestMethods;

namespace ZAAL_SQL.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly ZAAL_SQL.Data.ZAAL_SQLContext _context;

        public IndexModel(ZAAL_SQL.Data.ZAAL_SQLContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
        

public async Task<IActionResult> OnPostSetWatchedAsync(int id, bool? watched)
{
    var movie = await _context.Movie.FindAsync(id);
    if (movie != null)
    {
        movie.Watched = watched;
        await _context.SaveChangesAsync();
    }
    return RedirectToPage();
}
    }

    
}
