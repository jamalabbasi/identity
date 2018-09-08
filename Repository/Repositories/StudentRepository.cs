using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel;
using Infrastructure;

namespace Repository.Repositories
{
    public class StudentRepository
    {
        public StudentModel LoadStudentData(int id)
        {
            try
            {
                using (var dbContext = new MyTestingDbEntities(ConnectionStringManager.ConnectionStringEF))
                {
                    var oStudent = dbContext.Students.SingleOrDefault(s => s.Id == id);
                    var oStudentModel = new StudentModel();
                    if (oStudent != null)
                    {
                        oStudentModel.Id = oStudent.Id;
                        oStudentModel.Name = oStudent.Name;
                    }

                    return oStudentModel;
                }
            }
            catch (Exception ex)
            {
                int count = 0;
            }

            return null;
        }
    }
}
