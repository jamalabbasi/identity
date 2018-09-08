using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Repository.DomainModel;
using Repository.Repositories;

namespace WEBAPI.Controllers
{
    public class StudentController : ApiController
    {

        [HttpGet]
        [Route("api/Student/{id}")]
        public StudentModel Get(int id)
        {
            try
            {
                var _studentRepository = new StudentRepository();
                StudentModel student = _studentRepository.LoadStudentData(id);
                if (student == null) return null;
                return student;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
