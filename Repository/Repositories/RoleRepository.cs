using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Repository.Repositories
{
    public class RoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MyTestingDbEntities _db;

        public RoleRepository()
        {
            _roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(
                    new MyTestingDbEntities(ConnectionStringManager.ConnectionString)));
            _db = new MyTestingDbEntities(ConnectionStringManager.ConnectionString);
        }
        public void Create(List<string> roles)
        {
            foreach (string role in roles)
            {
                if (!_db.AspNetRoles.Any(r => r.Name == role))
                {
                    var identityRole = new IdentityRole()
                    {
                        Name = role,
                    };
                    _roleManager.Create(identityRole);
                }
            }
        }
        public string AdminRoleId()
        {
            return _roleManager.FindByName("Admin").Id;
        }
    }
}
