using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace MessageBoardBackend.Controllers
{
    public class JwtPakcet
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
    }
    [Produces("application/json")]
    [Route("auth")]
    public class AuthController : Controller
    {
        readonly ApiContext context;
        public AuthController(ApiContext context)
        {
            this.context = context;
        }

        [HttpPost("register")]
        public JwtPakcet Register([FromBody]Models.User user )
        {
            var jwt = new JwtSecurityToken();
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            context.Users.Add(user);
            context.SaveChanges();

            return new JwtPakcet() { Token = encodedJwt, FirstName = user.FirstName };
        }
    }
}