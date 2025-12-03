using Microsoft.AspNetCore.Mvc;

using MoviesAPI.Service;
namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeriesController : ControllerBase
    {
        private readonly IserieService _serieService;
        public SeriesController(IserieService serieService)
        {
            _serieService = serieService;
        }
        [HttpGet(Name = "GetSeries")]
        public List<Models.Serie> GetName()
        {
            return _serieService.GetSeries();
        }
    }
}