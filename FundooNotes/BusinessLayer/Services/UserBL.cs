using BusinessLayer.Interface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public void RegisterUser(UserPostModel userPostModel)
        {

            try
            {
                userRL.RegisterUser(userPostModel);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
                return userRL.Login(userLogin);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ForgetPassword(string email)
        {
            try
            {
                return userRL.ForgetPassword(email);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ResetPassword(string email, string Password)
        {
            try
            {
                userRL.ResetPassword(email, Password);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<UserModel> GetAllUsers()
        {
            try
            {
                return userRL.GetAllUsers();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
    
}
