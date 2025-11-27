using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Plugins;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;
using Microsoft.Data.SqlClient;

namespace ZAAL_SQL.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly ZAAL_SQLContext _context;

        public IList<Movie> Movie { get; set; } = new List<Movie>();

        public PrivacyModel(ILogger<PrivacyModel> logger, ZAAL_SQLContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            try
            {
                Movie = await _context.Movie.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Loading movies failed.c");
            }
        }
    }

}
