using MoviesAPI.Repository;
namespace MoviesAPI.Service
{
    public interface ImovieService
    {
        List<Models.Movie> GetMovies();
    }
    public class movieService: ImovieService
    {
        private readonly ImovieRepository _movieRepository;
        public movieService(ImovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public List<Models.Movie> GetMovies() { 
            return _movieRepository.GetMovies();
        }
    }
}
