using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libraryVueApp.Data;
using libraryVueApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libraryVueApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IEnumerable<User> Get()
        {
            var users = _userRepository.GetAllUsers();
            return users;
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
