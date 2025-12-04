using Microsoft.AspNetCore.Mvc;

using MoviesAPI.Service;
namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LFSController : ControllerBase
    {
        private readonly ILFSService _LFSService;
        public LFSController(ILFSService LFSService)
        {
            _LFSService = LFSService;
        }
        [HttpGet(Name = "GetLFS")]
        public List<Models.LFS> GetName()
        {
            return _LFSService.GetLFS();
        }
    }
}