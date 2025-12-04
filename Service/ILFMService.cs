using MoviesAPI.Repository;
namespace MoviesAPI.Service
{
    public interface ILFMService
    {
        List<Models.LFM> GetLFM();
    }
    public class LFMService : ILFMService
    {
        private readonly ILFMRepository _LFMRepository;
        public LFMService(ILFMRepository LFMRepository)
        {
            _LFMRepository = LFMRepository;
        }
        public List<Models.LFM> GetLFM()
        {
            return _LFMRepository.GetLFM(); 
        }
    }
}
