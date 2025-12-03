using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Pages.Series
{
    public class DetailsModel : PageModel
    {
        private readonly ZAAL_SQL.Data.Context _context;

        public DetailsModel(ZAAL_SQL.Data.Context context)
        {
            _context = context;
        }

        public Serie Serie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _context.Serie.FirstOrDefaultAsync(m => m.ID == id);
            if (serie == null)
            {
                return NotFound();
            }
            else
            {
                Serie = serie;
            }
            return Page();
        }
    }
}
