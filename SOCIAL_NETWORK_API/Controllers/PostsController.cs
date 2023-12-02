using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOCIAL_NETWORK_API.Models;

namespace SOCIAL_NETWORK_API.Controllers
{
    //[EnableCors("ReglasCors")]
    [Route("/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public readonly SocialNetworkContext _dbcontext;

        public PostsController(SocialNetworkContext _context)
        {
            _dbcontext = _context;
        }

        [HttpPost]
        [Route("/posts")]
        public IActionResult publishPost([FromBody] Post post)
        {
            try
            {
                var listofIDs = _dbcontext.Users.Select(u => u.UserId).ToList();
                var postPublished = _dbcontext.Posts.Where(p => p.UserId == post.UserId).Where(p => p.Text == post.Text).FirstOrDefault();
                if (!listofIDs.Contains(post.UserId))
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "The user "+post.UserId+" do not exists" });
                }
                else if (postPublished != null)
                {
                    return StatusCode(StatusCodes.Status208AlreadyReported, new { message = "The post of user " + post.UserId + " and with text "+post.Text+" already exists" });
                }
                else
                {
                    _dbcontext.Add(post);
                    _dbcontext.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK, new { post.PostId });
                }                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("/walls/{userID:int}")]
        public IActionResult getWall(int userID)
        {
            var listOfIDS = _dbcontext.Users.Select(u => u.UserId).ToList();
            List<Post> userWall = new List<Post>();
            List<Post> postWall = new List<Post>();
            List<Network> relations = new List<Network>();
            List<int> usersID = new List<int>();
            Post postAux = new Post();
            try
            {
                if (!listOfIDS.Contains(userID))
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "User " + userID + " not exists" });
                }
                relations = _dbcontext.Networks.ToList().Where(r => r.User1Id == userID).ToList();
                foreach (var r in relations)
                {
                    usersID.Add(r.User2Id);
                }
                postWall = _dbcontext.Posts.ToList();
                foreach (var p in postWall)
                {
                    foreach (var user in usersID)
                    {
                        if (p.UserId == user)
                        {
                            userWall.Add(p);
                        }
                    }
                    if (p.UserId == userID)
                    {
                        userWall.Add(p);
                    }
                }
                List<User> usuarios = _dbcontext.Users.ToList();
                List<Post> posts = _dbcontext.Posts.Where(p => p.UserId == userID).ToList();
                var orderPost = userWall.
                    Select(p => new { p.Text, p.PostedOn, fullName = usuarios.FirstOrDefault(n => n.UserId == p.UserId)?.FullName, likes = _dbcontext.PostLikeds.Where(pl => pl.PostLiked1 == p.PostId).Where(pl => pl.LikeStatus == true).Count()}).
                    OrderByDescending(post => post.PostedOn).ToList();

                return StatusCode(StatusCodes.Status200OK, new { orderPost });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }
    }
}
