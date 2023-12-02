using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOCIAL_NETWORK_API.Controllers;
using SOCIAL_NETWORK_API.Models;
using Xunit;

namespace SOCIAL_NETWORK_TEST
{
    public class UserTests
    {
        private readonly UserController _controller;
        private readonly SocialNetworkContext _context;

        public UserTests()
        {
            var options = new DbContextOptionsBuilder<SocialNetworkContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDB")
                .Options;
            _context = new SocialNetworkContext(options);
            _controller = new UserController(_context);
        }

        [Fact]
        public void ADD_OK()
        {
            var usuario = new User { UserId = 100, FullName = "Test User" };            
            var result = _controller.add(usuario) as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
    }
}