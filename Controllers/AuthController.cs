using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using libraryVueApp.Model.Auth;
using libraryVueApp.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libraryVueApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public ActionResult<AuthResultModel> Post([FromBody] AuthRequestModel model)
        {
            // NEVER DO THIS, JUST SHOWING THE EXAMPLE
            if (model.Username == "verybasic"
              && model.Password == "auth")
            {
                var result = new AuthResultModel()
                {
                    Success = true
                };

                // Never do this either, hardcoded strings
                var token = TokenSecurity.GenerateJwt(model.Username);

                result.Token = new JwtSecurityTokenHandler().WriteToken(token);
                result.Expiration = token.ValidTo;

                return Created("", result);

            }

            return BadRequest("Unknown failure");
        }
    }
}