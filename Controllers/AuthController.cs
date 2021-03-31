using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using libraryVueApp.Data;
using libraryVueApp.Dtos;
using libraryVueApp.Model;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult<AuthResultModel> Post([FromBody] AuthRequestModel model)
        {
            User user = _userRepository.GetUserByLogin(model.Login);
            if (user == null)
                return Unauthorized("User does not exist");

            if (_userRepository.Login(model.Login, model.Password))
            {
                var result = new AuthResultModel()
                {
                    Success = true
                };

                var token = TokenSecurity.GenerateJwt(model.Login);
                result.Token = new JwtSecurityTokenHandler().WriteToken(token);
                result.Expiration = token.ValidTo;

                result.Name = $"{user.Firstname} {user.Lastname}";
                result.Roles = user.Roles.Select(o => o.RoleId.ToString()).ToArray();
                result.UserId = user.Id;

                return Created("", result);

            }

            return Unauthorized("Wrong login or password");
        }
        [HttpPost]
        [Route("register/")]
        public ActionResult Register([FromBody] CreateUserDto userdto)
        {
            //todo move to sql composite key
            if (_userRepository.LoginAlreadyExists(userdto.Login))
                return BadRequest("Login already taken!");

            User user = _mapper.Map<User>(userdto);
            _userRepository.AddNewUser(user);
            _userRepository.SaveChanges();

            return NoContent();
        }
    }
}