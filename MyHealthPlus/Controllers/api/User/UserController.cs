using Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using MyHealthPlusWeb.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Entity.User;
using Business.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyHealthPlusWeb.Controllers.api.User
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public UserController(IUserService userService
            
            )
        {
            this._userService = userService;
        }

        private IUserService _userService;


        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
           var response =  _userService.GetUserById(id);

            return Ok(response);
        }

        // POST api/<UserController>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserInfo request)
        {
            _userService.InsertUser(request);

            return Ok(1);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Email or Password is incorrect" });

            return Ok(response);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
