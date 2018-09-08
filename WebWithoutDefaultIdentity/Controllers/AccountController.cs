using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Infrastructure;
using WebWithoutDefaultIdentity.RequestModels;

namespace WebWithoutDefaultIdentity.Controllers
{
    [Route("api/account")]
    public class AccountController : ApiController
    {
        private readonly AccountRepository _accountRepository;
        private readonly UserRepository _userRepository;

        public AccountController()
        {
            _accountRepository = new AccountRepository();
            _userRepository = new UserRepository();
        }

        [HttpPost]
        [Route("api/account/Register")]
        public IHttpActionResult Register([FromBody] UserRequest model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                ApplicationUser newUser = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    RegistrationDate = model.RegistrationDate,
                    ExpiryDate = model.ExpiryDate,
                    StudentId = model.StudentId
                };
                if (_accountRepository.Register(newUser, model.Password, model.Role))
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("api/account/{id}")]
        public IHttpActionResult Put(string id, [FromBody] UserRequest model)
        {
            try
            {
                if (_userRepository.EditUser(id, model.Email, model.FirstName, model.Password))
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("api/account/{id}")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                if (_userRepository.DeleteUser(id))
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("api/account/Logout")]
        public IHttpActionResult Logout()
        {
            try
            {
                _accountRepository.Logout();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}