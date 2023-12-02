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
    public class PostTests
    {
        private readonly PostsController _controller;
        private readonly SocialNetworkContext _context;

        public PostTests()
        {
            var options = new DbContextOptionsBuilder<SocialNetworkContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDB")
                .Options;
            _context = new SocialNetworkContext(options);
            _controller = new PostsController(_context);
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
        public void PUBLISHPOST()
        {
            var UserID = 1;
            var listOfIDS = _context.Users.Select(u => u.UserId).ToList();
            var post = new Post { UserId = UserID, Text = "Test Post", Visibility = "public", PostedOn = DateTime.Now };
            var postp = _context.Posts.Where(p => p.UserId == post.UserId).Where(p => p.Text == post.Text).FirstOrDefault();
            if (!listOfIDS.Contains(UserID))
            {
                var result404 = _controller.publishPost(post) as ObjectResult;
                Assert.NotNull(result404);
                Assert.Equal(StatusCodes.Status404NotFound, result404.StatusCode);
            }
            else if (postp != null)
            {
                var result208 = _controller.publishPost(post) as ObjectResult;
                Assert.NotNull(result208);
                Assert.Equal(StatusCodes.Status208AlreadyReported, result208.StatusCode);
            }
            else
            {
                var result = _controller.publishPost(post) as ObjectResult;
                Assert.NotNull(result);
                Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            }
        }

        [Fact]
        public void GETWALL()
        {
            int UserID = 11;
            var listOfIDS = _context.Users.Select(u => u.UserId).ToList();
            if (!listOfIDS.Contains(UserID))
            {
                var result404 = _controller.getWall(UserID) as ObjectResult;
                Assert.NotNull(result404);
                Assert.Equal(StatusCodes.Status404NotFound, result404.StatusCode);
            }
            else
            {
                var result = _controller.getWall(UserID) as ObjectResult;
                Assert.NotNull(result);
                Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            }
        }
    }
}
