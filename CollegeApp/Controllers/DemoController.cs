using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("apli/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //1.strongley coupled 

        //private readonly IMyLogger _mylogger;

        //public DemoController()
        //{
        //    _mylogger = new LogToFile();
        //}
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    _mylogger.Log("index method started");
        //    return Ok();
        //}

        //2.looosley coupled 

        //private readonly IMyLogger _mylogger;

        //public DemoController(IMyLogger myLogger)
        //{
        //    _mylogger = myLogger;
        //}
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    _mylogger.Log("index method started");
        //    return Ok();
        //}

        private readonly ILogger<DemoController> _logger;
        public DemoController(ILogger<DemoController> logger) 
        {
            _logger = logger;
        }
        [HttpGet]

        public ActionResult Index() 
        {
            _logger.LogTrace("log message from logtarce method");
            _logger.LogDebug("log message from LogDebug method");
            _logger.LogInformation("log message from LogInformation method");
            _logger.LogWarning("log message from LogWarning method");
            _logger.LogError("log message from LogError method");
            _logger.LogCritical("log message from LogCritical method");
            
            
            
            return Ok();
        }
    }
}
