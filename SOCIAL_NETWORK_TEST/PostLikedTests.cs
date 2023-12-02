using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SOCIAL_NETWORK_API.Controllers;
using SOCIAL_NETWORK_API.Models;
using Xunit;

namespace SOCIAL_NETWORK_TEST
{
    public class PostLikedTests
    {
        private readonly PostLikedController _controller;
        private readonly SocialNetworkContext _context;

        public PostLikedTests()
        {
            var options = new DbContextOptionsBuilder<SocialNetworkContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDB")
                .Options;
            _context = new SocialNetworkContext(options);
            _controller = new PostLikedController(_context);
            AddFakeData();
        }

        public void AddFakeData()
        {
            var fakeUser = new Faker<User>()
                .RuleFor(u => u.UserId, f => f.IndexFaker + 1)
                .RuleFor(u => u.FullName, f => f.Person.FullName);

            var fakeUsers = fakeUser.Generate(10);

            var fakePost = new Faker<Post>()
                .RuleFor(p => p.PostId, f => f.IndexFaker + 1)
                .RuleFor(p => p.UserId, f => f.PickRandom(fakeUsers).UserId)
                .RuleFor(p => p.Text, f => f.Lorem.Sentence())
                .RuleFor(p => p.Visibility, "public")
                .RuleFor(p => p.PostedOn, DateTime.Now);

            var fakePosts = fakePost.Generate(10);

            _context.Users.AddRange(fakeUsers);
            _context.SaveChanges();
        }

        [Fact]
        public void LIKEPOST()
        {
            var UserID = 1;
            var PostL = 2;
            var listOfIDS = _context.Users.Select(u => u.UserId).ToList();
            var listOfPosts = _context.Posts.Select(p => p.PostId).ToList();
            if (!listOfIDS.Contains(UserID) || !listOfPosts.Contains(PostL))
            {
                var result404 = _controller.likePost(UserID, PostL) as ObjectResult;
                Assert.NotNull(result404);
                Assert.Equal(StatusCodes.Status404NotFound, result404.StatusCode);
            }
            else
            {
                var result = _controller.likePost(UserID, PostL) as ObjectResult;
                Assert.NotNull(result);
                Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            }
        }

        [Fact]
        public void UNLIKEPOST()
        {
            var UserID = 1;
            var PostL = 2;
            var listOfIDS = _context.Users.Select(u => u.UserId).ToList();
            var listOfPosts = _context.Posts.Select(p => p.PostId).ToList();
            if (!listOfIDS.Contains(UserID) || !listOfPosts.Contains(PostL))
            {
                var result404 = _controller.unlikePost(UserID, PostL) as ObjectResult;
                Assert.NotNull(result404);
                Assert.Equal(StatusCodes.Status404NotFound, result404.StatusCode);
            }
            else
            {
                var result = _controller.unlikePost(UserID, PostL) as ObjectResult;
                Assert.NotNull(result);
                Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            }
        }
    }
}
