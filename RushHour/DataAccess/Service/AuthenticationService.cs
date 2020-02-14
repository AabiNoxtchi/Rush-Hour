using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class AuthenticationService
    {
        public User LoggedUser { get; private set; }

        public void AuthenticateUser(string email, string password)
        {
            UsersRepository userRepo = new UsersRepository();
            LoggedUser = userRepo.GetAll(u => u.Email == email && u.Password == password && u.IsEmailVerified).FirstOrDefault();
        }
    }
}
