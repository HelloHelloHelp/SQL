using MoviesAPI.Repository;
namespace MoviesAPI.Service
{
    public interface IserieService
    {
        List<Models.Serie> GetSeries();
    }
    public class serieService : IserieService
    {
        private readonly IserieRepository _serieRepository;
        public serieService(IserieRepository serieRepository)
        {
            _serieRepository = serieRepository;
        }
        public List<Models.Serie> GetSeries()
        {
            return _serieRepository.GetSeries();
        }
    }
}
