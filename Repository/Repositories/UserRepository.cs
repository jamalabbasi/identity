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
    public class UserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleRepository _roleRepository;
        public UserRepository()
        {
            _userManager = new UserManager<ApplicationUser>(
                   new UserStore<ApplicationUser>(
                       new MyTestingDbEntities(ConnectionStringManager.ConnectionString)));
            _roleRepository = new RoleRepository();
        }

        public bool IsEmailAlreadyExist(string email, ref string strErrorMessage)
        {
            var isValid = false;
            if (_userManager.FindByEmail(email) != null)
            {
                isValid = true;
                strErrorMessage = "Email Already Exists";
            }
            return isValid;
        }

        public ApplicationUser LoadUser(string id)
        {
            ApplicationUser user = _userManager.FindById(id);
            if (user == null) return null;
            return user;
        }

        public dynamic SearchUser()
        {
            List<ApplicationUser> users = _userManager.Users.ToList();
            if (users == null) return null;
            return users;
        }

        public dynamic Admins()
        {
            string adminRoleId = _roleRepository.AdminRoleId();
            List<ApplicationUser> users = _userManager.Users.Where(u => u.Roles.FirstOrDefault().RoleId == adminRoleId).ToList();
            if (users == null) return null;
            return users;
        }

        public bool DeleteUser(string id)
        {
            ApplicationUser user = _userManager.FindById(id);
            if (user == null) return false;
            var result = _userManager.Delete(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public bool EditUser(string id, string email, string firstName, string password)
        {
            ApplicationUser user = _userManager.FindById(id);
            user.Email = email;
            user.FirstName = firstName;

            var result = _userManager.Update(user);
            if (result.Succeeded)
            {
                var removePassword = _userManager.RemovePassword(user.Id);
                if (removePassword.Succeeded)
                {
                    var passwordAdded = _userManager.AddPassword(user.Id, password);
                    if (passwordAdded.Succeeded) return true;
                }
                return false;
            }
            return false;
        }

        public bool IsValidUserEmail(string email)
        {
            ApplicationUser user = _userManager.FindByEmail(email);
            if (user == null) return false;
            return true;
        }

        public string LoadUserPasswordByEmail(string email)
        {
            ApplicationUser user = _userManager.FindByEmail(email);
            if (user == null) return null;
            var result = _userManager.RemovePassword(user.Id);
            if (result.Succeeded)
            {
                var passwordAdded = _userManager.AddPassword(user.Id, "Password@1");
                if (passwordAdded.Succeeded) return "Password@1";
            }
            return "";
        }
    }
}
