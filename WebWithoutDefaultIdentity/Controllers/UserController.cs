using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Repository.Repositories;

namespace WebWithoutDefaultIdentity.Controllers
{
    [Route("api/user")]
    public class UserController : ApiController
    {
        private readonly UserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        [HttpGet]
        [Route("api/account/GetAll")]
        public IHttpActionResult Get()
        {
            try
            {
                var users = _userRepository.SearchUser();
                if (users != null) return Ok(users);
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
