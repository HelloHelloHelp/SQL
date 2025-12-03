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

namespace ZAAL_SQL.Pages.Series
{
    public class IndexModel : PageModel
    {
        private readonly ZAAL_SQL.Data.Context _context;

        public IndexModel(ZAAL_SQL.Data.Context context)
        {
            _context = context;
        }

        public IList<Serie> Serie { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Serie = await _context.Serie.ToListAsync();
        }


        public async Task<IActionResult> OnPostSetWatchedAsync(int id, string? watched)
        {
            var serie = await _context.Serie.FindAsync(id);
            if (serie != null)
            {
                serie.Watched = string.IsNullOrEmpty(watched) ? null : watched;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }


}
