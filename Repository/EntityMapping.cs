using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Repository.DomainModel;
using AutoMapper;

namespace Repository
{
    class EntityMapping
    {
        public static void CreateEntityMapping()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Student, StudentModel>();
                cfg.CreateMap<ApplicationUser, ApplicationUser>();
            });
        }
    }
}
