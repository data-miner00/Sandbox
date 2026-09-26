namespace Sandbox.DataProtection.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sandbox.DataProtection.Extensions;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private List<User> users = new List<User>();

        public UserController(ILogger<UserController> logger)
        {
            users.Add(new User
            {
                Id = 1,
                FirstName = "Hello",
                LastName = "World",
                Email = "hello@example.com",
                PhoneNumber = "123 456 789",
                Address = "34 Sky Avenue, Queensland",
                DateOfBirth = new DateTime(1990, 2, 12),
            });
            this.logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = this.users.Find(x => x.Id == id);

            if (user is null)
            {
                return this.NotFound();
            }

            this.logger.LogUserObtained(user);

            return user;
        }
    }
}
