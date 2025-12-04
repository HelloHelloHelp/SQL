using MoviesAPI.Repository;
namespace MoviesAPI.Service
{
    public interface ILFSService
    {
        List<Models.LFS> GetLFS();
    }
    public class LFSService : ILFSService
    {
        private readonly ILFSRepository _LFSRepository;
        public LFSService(ILFSRepository LFSRepository)
        {
            _LFSRepository = LFSRepository;
        }
        public List<Models.LFS> GetLFS()
        {
            return _LFSRepository.GetLFS();
        }
    }
}
