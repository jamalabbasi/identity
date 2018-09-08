using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Repository.Repositories;

namespace WebWithoutDefaultIdentity.Controllers
{
        [Route("api/role")]
        public class RoleController : ApiController
        {
            private readonly RoleRepository _roleRepository;

            public RoleController()
            {
                _roleRepository = new RoleRepository();
            }
            [HttpPost]
            [Route("api/role/create")]
            public void Create(List<string> roles)
            {

                try
                {
                    _roleRepository.Create(roles);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
