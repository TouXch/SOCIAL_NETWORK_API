using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOCIAL_NETWORK_API.Models;

namespace SOCIAL_NETWORK_API.Controllers
{
    //[EnableCors("ReglasCors")]
    [Route("/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly SocialNetworkContext _dbcontext;

        public UserController(SocialNetworkContext _context)
        {
            _dbcontext = _context;
        }

        [HttpPost]
        [Route("users")]
        public IActionResult add([FromBody] User usuario)
        {
            try
            {
                _dbcontext.Add(usuario);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { userID = usuario.UserId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }
    }
}
