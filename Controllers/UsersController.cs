using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libraryVueApp.Data;
using libraryVueApp.Dtos.UserDtos;
using libraryVueApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libraryVueApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookOrderRepository _bookOrderRepository;

        public UsersController(IUserRepository userRepository, IBookOrderRepository bookOrderRepository)
        {
            _userRepository = userRepository;
            _bookOrderRepository = bookOrderRepository;
        }
        // GET: api/User
        [HttpGet]
        public IEnumerable<UserViewModel> Get()
        {
            var users = _userRepository.GetAllUsers(includeRoles: true);

            IEnumerable<UserViewModel> userVms = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Login = u.Login,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Roles = string.Join(",", u.Roles.Select(r => r.RoleId.ToString()))
            });

            return userVms;
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost("{id}/assignlibrarian")]
        public ActionResult Post(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) ;
            NotFound("User with given id was not found");

            _userRepository.AssignRole(user, RoleType.Librarian);            

            return Ok();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
                return NotFound("User with given id was not found");

            _userRepository.Delete(user);

            return Ok();
        }

        [HttpGet("{id}/hasoverduebooks")]
        public ActionResult<HasOverDueBooksResult> CheckIfUserHasOverdueBooks(int id)
        {
            User user = _userRepository.GetUserById(id);
            if (user == null)
                return BadRequest("Wrong userId");

            HasOverDueBooksResult result = _bookOrderRepository.UserHasOverdueBookOrders(id);

            return Ok(result);
        }
    }
}
