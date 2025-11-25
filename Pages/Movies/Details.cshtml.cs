using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly ZAAL_SQL.Data.ZAAL_SQLContext _context;

        public DetailsModel(ZAAL_SQL.Data.ZAAL_SQLContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                Movie = movie;
            }
            return Page();
        }
    }
}
