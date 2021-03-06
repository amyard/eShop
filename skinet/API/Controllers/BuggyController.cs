using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretKey()
        {
            return "secret stuff";
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequuest()
        {
            var thing = _context.Products.Find(42);
            if(thing == null)
            {
                return  NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var thing = _context.Products.Find(42);
            var thingToReturn = thing.ToString();       // will generate Exception
            return Ok();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        
        [HttpGet("badrequest/{id}")]
        public IActionResult GetNotFoundRequuest(int id)
        {
            return Ok();
        }
    }
}