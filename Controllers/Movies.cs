using Microsoft.AspNetCore.Mvc;

using MoviesAPI.Service;
namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ImovieService _movieService;
       public MoviesController(ImovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet(Name = "GetMovies")]
        public List<Models.Movie> GetName()
        {
            return _movieService.GetMovies();
        }
    }
}