using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using Microsoft.Owin.Security.OAuth;

namespace Repository.Repositories
{
    public class AccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private IAuthenticationManager Authentication
        {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }

        public AccountRepository()
        {
            _userManager = new UserManager<ApplicationUser>(
                   new UserStore<ApplicationUser>(
                       new MyTestingDbEntities(ConnectionStringManager.ConnectionString)));
            _roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(
                    new MyTestingDbEntities(ConnectionStringManager.ConnectionString)));
        }
        public bool Register(ApplicationUser user, string password, string role)
        {
            var result = _userManager.Create(user, password);
            if (result.Succeeded)
            {
                var findUser = _userManager.FindByEmail(user.Email);
                _userManager.AddToRole(findUser.Id, role);
                return true;
            }
            return false;
        }



        public bool IsValidLogin(string email, string password, ref string role)
        {
            var user = _userManager.FindByEmail(email);
            if (user != null)
            {
                role = GetRoleByUser(user);
                var hasher = new PasswordHasher();
                return hasher.VerifyHashedPassword(user.PasswordHash, password).GetHashCode() == 1;
            }
            else return false;
        }

        public void Logout()
        {
            Authentication.SignOut(OAuthDefaults.AuthenticationType);
        }

        public ApplicationUser GetUser(string email)
        {
            using (var _userManager = new UserManager<ApplicationUser>(
                   new UserStore<ApplicationUser>(
                       new MyTestingDbEntities(ConnectionStringManager.ConnectionString))))
            {

                return _userManager.FindByEmail(email);
            }
        }

        public IdentityRole GetRole(string role)
        {
            return _roleManager.FindByName(role);
        }

        public string GetRoleByUser(ApplicationUser user)
        {
            var roleId = user.Roles.FirstOrDefault().RoleId;
            var role = _roleManager.FindById(roleId);
            if (role == null) return "";
            return role.Name;
        }
        public ApplicationUser CurrentLoggedUser()
        {
            return GetUser(HttpContext.Current.User.Identity.Name);
        }

        public string CurrentLoggedUserRole()
        {
            ApplicationUser user = GetUser(HttpContext.Current.User.Identity.Name);
            if (user != null)
            {
                return GetRoleByUser(user);
            }
            return "";
        }
    }
}
