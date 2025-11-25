using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZAAL_SQL.Data;
using ZAAL_SQL.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace ZAAL_SQL.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        public IList<Movie> Movie { get; set; } = new List<Movie>();

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }

}
