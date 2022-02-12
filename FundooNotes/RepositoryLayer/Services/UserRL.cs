using CommonLayer.User;
using Experimental.System.Messaging;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Message = Experimental.System.Messaging.Message;

namespace RepositoryLayer.Class
{

    public class UserRL : IUserRL
    {
        FundooDbContext dbContext;
        public UserRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                UserModel user = new UserModel();
                user.Userid = new UserModel().Userid;
                user.Fname = userPostModel.Fname;
                user.Lname = userPostModel.Lname;
                user.Phone = userPostModel.Phone;
                user.Adress = userPostModel.Adress;
                user.email = userPostModel.email;
                StringCipher.Encrypt(userPostModel.Password);
                user.Password = StringCipher.Encrypt(userPostModel.Password);

                
                user.RegisteredDate = DateTime.Now;
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
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
                UserModel user = new UserModel();
                var result = dbContext.Users.Where(x => x.email == userLogin.email && x.Password == userLogin.Password).FirstOrDefault();
                if (result != null)
                    return GenerateJWTToken(userLogin.email, user.Userid);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateJWTToken(string email, int Userid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email.ToString()),
                    new Claim("userId", Userid.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ForgetPassword(string email)
        {
            try
            {
                UserModel checkemail = dbContext.Users.FirstOrDefault(e => e.email == email);
               
                if (checkemail != null)
                {
                    MessageQueue queue;
                    //ADD MESSAGE TO QUEUE
                    if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                    {
                        queue = new MessageQueue(@".\Private$\FundooQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\FundooQueue");
                    }

                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = GenerateJWTToken(email, checkemail.Userid);
                    MyMessage.Label = "Forget Password Email";
                    queue.Send(MyMessage);
                    Message msg = queue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailServices.sendMail(email, msg.Body.ToString());
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    queue.BeginReceive();
                    queue.Close();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        public void ResetPassword(string email, string Password)
        {
            try
            {
                UserModel user = new UserModel();
                var result = dbContext.Users.FirstOrDefault(x => x.email == email);
                if (result != null)
                {
                    result.Password = Password;
                    dbContext.SaveChanges();
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UserModel> GetAllUsers()
        {
            try
            {
                var result = dbContext.Users.ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailServices.sendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
               // Handle other sources of MessageQueueException.
            }

        }


    }

}







