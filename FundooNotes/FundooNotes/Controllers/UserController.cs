using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        FundooDbContext fundooDbContext;
        IUserBL userBL;
        public UserController(IUserBL userBL, FundooDbContext fundooDB)
        {
            this.userBL = userBL;
            this.fundooDbContext = fundooDB;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }

       
        [HttpPost("Register")]
        public ActionResult RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.RegisterUser(userPostModel);
                return this.Ok(new { success = true, message = $"Registration Successful {userPostModel.email}" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost("Login")]
        public ActionResult Login(UserLogin userlogin)
        {
            try
            {
                var result = this.userBL.Login(userlogin);
                if (result != null)
                    return this.Ok(new { success = true, message = $"login successful {userlogin.email}, token = {result}" });
                else
                    return this.BadRequest(new { success = false, message = "invalid username and password" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut("ForgetPassword")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                var result = fundooDbContext.Users.FirstOrDefault(x => x.email == email);
                if (result == null)
                {
                    return this.Ok(new { success = false, message = $"Email not registered" });
                }
                else
                {
                    this.userBL.ForgetPassword(email);
                    return this.BadRequest(new { success = true, message = $"Tokken sent for resetting Password" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut("ResetPassword")]
        public ActionResult ResetPassword(string Password)
        {
            try
            {

                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var UserEmailObject = claims.Where(p => p.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").FirstOrDefault()?.Value;
                    if (UserEmailObject == null)
                    {
                        return this.BadRequest(new { success = false, message = $"Password not Reset" });
                    }
                    else
                    {
                        this.userBL.ResetPassword(UserEmailObject, Password);
                        return Ok(new { success = true, message = "Password Reset Sucessfully" });
                    }
                }

                return this.BadRequest(new { success = false, message = $"Password not Changed" });
            
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        [HttpGet("GetAllUsers")]
        public ActionResult GetAllUsers()
        {
            try
            {
                var result = this.userBL.GetAllUsers();
                return this.Ok(new { success = true, message = $"below are the user data", data = result });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
    





 
