using Microsoft.AspNetCore.Mvc;

using MoviesAPI.Service;
namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LFMController : ControllerBase
    {
        private readonly ILFMService _LFMService;
        public LFMController(ILFMService LFMService)
        {
            _LFMService = LFMService;
        }
        [HttpGet(Name = "GetLFM")]
        public List<Models.LFM> GetName()
        {
            return _LFMService.GetLFM();
        }
    }
}