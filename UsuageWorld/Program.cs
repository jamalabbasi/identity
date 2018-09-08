using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel;
using Repository.Repositories;
namespace UsuageWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentRepository sr=new StudentRepository();
            StudentModel sm = sr.LoadStudentData(1);
            Console.WriteLine(sm.Name);
        }
    }
}
