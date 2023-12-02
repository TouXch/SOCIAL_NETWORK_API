using Bogus;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOCIAL_NETWORK_API.Controllers;
using SOCIAL_NETWORK_API.Models;
using System.Diagnostics;
using Xunit;
using Xunit.Sdk;

namespace SOCIAL_NETWORK_TEST
{
    public class NetworkTests
    {
        private readonly networkController _controller;
        private readonly SocialNetworkContext _context;

        public NetworkTests()
        {
            var options = new DbContextOptionsBuilder<SocialNetworkContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDB")
                .Options;
            _context = new SocialNetworkContext(options);
            _controller = new networkController(_context);
            AddFakeData();
        }

        public class NetworkEqualityComparer : IEqualityComparer<Network>
        {
            public bool Equals(Network x, Network y)
            {
                return x.FriendshipId == y.FriendshipId;
            }

            public int GetHashCode(Network obj)
            {
                return obj.FriendshipId.GetHashCode();
            }
        }


        public void AddFakeData()
        {
            var fakeUser = new Faker<User>()
                .RuleFor(u => u.UserId, f => f.IndexFaker + 1)
                .RuleFor(u => u.FullName, f => f.Person.FullName);

            var fakeUsers = fakeUser.Generate(20);

            _context.Users.AddRange(fakeUsers);
            _context.SaveChanges();

            //Agregar relaciones de red falsas
           var fakeNetwork = new Faker<Network>()
               .RuleFor(n => n.FriendshipId, f => f.IndexFaker + 1)
               .RuleFor(n => n.User1Id, f => f.PickRandom(fakeUsers).UserId)
               .RuleFor(n => n.User2Id, f => f.PickRandom(fakeUsers).UserId)
               .RuleFor(n => n.RelationType, "friendship");

            var fakeNetworks = fakeNetwork.Generate(20);

            _context.Networks.AddRange(fakeNetworks);
            _context.SaveChanges();
        }

        [Fact]
        public void MAKEFRIEND()
        {
            int userID1 = 10;
            int userID2 = 11;
            var listOfIDS = _context.Users.Select(u => u.UserId).ToList();
            var relationship = _context.Networks.Where(n => n.User1Id == userID1).Where(n => n.User2Id == userID2).Select(n => n.RelationType).FirstOrDefault();
            if (!listOfIDS.Contains(userID1) || !listOfIDS.Contains(userID2))
            {
                var result404 = _controller.makeFriend(userID1, userID2) as ObjectResult;
                Assert.NotNull(result404);
                Assert.Equal(StatusCodes.Status404NotFound, result404.StatusCode);
            }
            else if (relationship != null)
            {
                var result208 = _controller.makeFriend(userID1, userID2) as ObjectResult;
                Assert.NotNull(result208);
                Assert.Equal(StatusCodes.Status208AlreadyReported, result208.StatusCode);
            }
            else
            {
                var result200 = _controller.makeFriend(userID1, userID2) as ObjectResult;
                Assert.NotNull(result200);
                Assert.Equal(StatusCodes.Status200OK, result200.StatusCode);
            }
        }

        [Fact]
        public void MAKEFOLLOW()
        {
            int userID1 = 15;
            int userID2 = 16;
            var listOfIDS = _context.Users.Select(u => u.UserId).ToList();
            var relationship = _context.Networks.Where(n => n.User1Id == userID1).Where(n => n.User2Id == userID2).Select(n => n.RelationType).FirstOrDefault();
            if (!listOfIDS.Contains(userID1) || !listOfIDS.Contains(userID2))
            {
                var resultF404 = _controller.followUser(userID1, userID2) as ObjectResult;
                Assert.NotNull(resultF404);
                Assert.Equal(StatusCodes.Status404NotFound, resultF404.StatusCode);
            }
            else if (relationship != null)
            {
                var resultF208 = _controller.followUser(userID1, userID2) as ObjectResult;
                Assert.NotNull(resultF208);
                Assert.Equal(StatusCodes.Status208AlreadyReported, resultF208.StatusCode);
            }
            else
            {
                var resultF200 = _controller.followUser(userID1, userID2) as ObjectResult;
                Assert.NotNull(resultF200);
                Assert.Equal(StatusCodes.Status200OK, resultF200.StatusCode);
            }
        }
    }
}
