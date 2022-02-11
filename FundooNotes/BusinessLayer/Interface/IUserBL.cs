using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
     public interface IUserBL
    {
      
        void RegisterUser(UserPostModel userPostModel);
        public string Login(UserLogin UserLogin);
        public bool ForgetPassword(string email);
        void ResetPassword(string email, string Password);
        List<UserModel> GetAllUsers();
    }
}
