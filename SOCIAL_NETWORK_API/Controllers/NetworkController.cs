using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOCIAL_NETWORK_API.Models;

namespace SOCIAL_NETWORK_API.Controllers
{
    //[EnableCors("ReglasCors")]
    [Route("/[controller]")]
    [ApiController]
    public class networkController : ControllerBase
    {
        public readonly SocialNetworkContext _dbcontext;

        public networkController(SocialNetworkContext _context)
        {
            _dbcontext = _context;
        }

        [HttpPost]
        [Route("/users/{userID:int}/friends/{friendID:int}")]
        public IActionResult makeFriend(int userID, int friendID)
        {
            try
            {
                var listofIDs = _dbcontext.Users.Select(u => u.UserId).ToList();
                Network relation = _dbcontext.Networks.Where(r => r.User1Id == userID).Where(r => r.User2Id == friendID).Where(r => r.RelationType == "friendship").FirstOrDefault();
                Network relationFollow = _dbcontext.Networks.Where(r => r.User1Id == userID).Where(r => r.User2Id == friendID).Where(r => r.RelationType == "follow").FirstOrDefault();
                if (!listofIDs.Contains(userID) || !listofIDs.Contains(friendID))
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "One of the users in the relationship not exists" });
                }
                else if (relation == null && relationFollow == null)
                {
                    Network relationship = new Network();
                    relationship.User1Id = userID;
                    relationship.User2Id = friendID;
                    _dbcontext.Add(relationship);
                    _dbcontext.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK, new { message = "Successfully completed the operation." });
                }
                else if (relationFollow != null)
                {
                    return StatusCode(StatusCodes.Status208AlreadyReported, new { message = "User " + friendID + " is follower of user " + userID });
                }
                else
                {
                    return StatusCode(StatusCodes.Status208AlreadyReported, new { message = "Relationship already exists." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("/users/{userID:int}/followers/{followerID:int}")]
        public IActionResult followUser(int userID, int followerID)
        {
            try
            {
                var listofIDs = _dbcontext.Users.Select(u => u.UserId).ToList();
                Network relation = _dbcontext.Networks.Where(r => r.User1Id == userID).Where(r => r.User2Id == followerID).Where(r => r.RelationType == "friendship").FirstOrDefault();
                Network relationFollow = _dbcontext.Networks.Where(r => r.User1Id == userID).Where(r => r.User2Id == followerID).Where(r => r.RelationType == "follow").FirstOrDefault();
                if (!listofIDs.Contains(userID) || !listofIDs.Contains(followerID))
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "One of the users in the relationship not exists" });
                }
                else if (relation == null && relationFollow == null)
                {
                    Network relationship = new Network();
                    relationship.User1Id = userID;
                    relationship.User2Id = followerID;
                    relationship.RelationType = "follow";
                    _dbcontext.Add(relationship);
                    _dbcontext.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK, new { message = "Successfully completed the operation." });
                }
                else if (relation != null)
                {
                    return StatusCode(StatusCodes.Status208AlreadyReported, new { message = "User " + followerID + " is friend of user " + userID });
                }
                else
                {
                    return StatusCode(StatusCodes.Status208AlreadyReported, new { message = "Relationship already exists" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }
    }
}
