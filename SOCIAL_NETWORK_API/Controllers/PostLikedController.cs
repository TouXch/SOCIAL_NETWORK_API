using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOCIAL_NETWORK_API.Models;

namespace SOCIAL_NETWORK_API.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("/[controller]")]
    [ApiController]
    public class PostLikedController : ControllerBase
    {
        public readonly SocialNetworkContext _dbcontext;

        public PostLikedController(SocialNetworkContext _context)
        {
            _dbcontext = _context;
        }

        [HttpPost]
        [Route("/{userID:int}/like/{postID:int}")]
        public IActionResult likePost(int userID, int postID)
        {
            try
            {
                PostLiked postL = new PostLiked();
                PostLiked pl = _dbcontext.PostLikeds.Where(p => p.PostLiked1 == postID).Where(p => p.UserLike == userID).Where(p => p.LikeStatus == true).FirstOrDefault();
                if (pl != null)
                {
                    return StatusCode(StatusCodes.Status208AlreadyReported, new { message = "Post are ready liked." });
                }
                else if ((pl = _dbcontext.PostLikeds.Where(p => p.PostLiked1 == postID).Where(p => p.UserLike == userID).Where(p => p.LikeStatus == false).FirstOrDefault()) != null)
                {
                    pl.LikeStatus = true;
                    _dbcontext.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK, new { message = "User " + userID + " has liked the post " + postID });
                }
                else
                {
                    postL.UserLike = userID;
                    postL.PostLiked1 = postID;
                    postL.LikeStatus = true;
                    _dbcontext.Add(postL);
                    _dbcontext.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK, new { message = "Like!!!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("/{userID:int}/unlike/{postID:int}")]
        public IActionResult unlikePost(int userID, int postID)
        {
            try
            {
                PostLiked pl = _dbcontext.PostLikeds.Where(p => p.PostLiked1 == postID).Where(p => p.UserLike == userID).FirstOrDefault();
                if (pl == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "User " + userID + " not liked post " + postID });
                }
                else
                {                    
                    pl.LikeStatus = false;
                    _dbcontext.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK, new { message = "User " + userID + " has unlike the post " + postID });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }
    }
}
