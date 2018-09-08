using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebWithoutDefaultIdentity.Controllers
{
    [Route("api/superadmin")]
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : ApiController
    {
        private readonly UserRepository _userRepository;

        public SuperAdminController()
        {
            _userRepository = new UserRepository();
        }
        // GET: api/SuperAdmin
        [HttpGet]
        [Route("api/superadmin/admins")]
        public IHttpActionResult Get()
        {
            try
            {
                var users = _userRepository.Admins();
                if (users != null) return Ok(users);
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: api/SuperAdmin/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SuperAdmin
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SuperAdmin/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SuperAdmin/5
        public void Delete(int id)
        {
        }
    }
}
