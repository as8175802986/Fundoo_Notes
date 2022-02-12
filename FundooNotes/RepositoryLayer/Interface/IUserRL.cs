using CommonLayer.User;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        
        public void RegisterUser(UserPostModel userPostModel);
        public void ResetPassword(string email, string Password);
        public bool ForgetPassword(string email);
        public string Login(UserLogin userlogin);
        List<UserModel> GetAllUsers();
        
    }
}
