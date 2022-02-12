using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace RepositoryLayer.Services
{
    public class EmailServices
    {

        public static void sendMail(string email, string token)
        {

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("rish0498@gmail.com", "Rishu@1234");

                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(email);
                msgObj.From = new MailAddress("rish0498@gmail.com.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.Body = $"FundooNotes/{token}";
                client.Send(msgObj);
            }
        }


        //public void MSMQSender(string token)
        //{
        //    messageQueue.Path = @".\private$\Token";//for windows path
        //    if (!MessageQueue.Exists(messageQueue.Path))
        //    {
        //        MessageQueue.Create(messageQueue.Path);
        //    }
        //    messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });//for asyn communication
        //    messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;//press tab 
        //    messageQueue.Send(token);
        //    messageQueue.BeginReceive();
        //    messageQueue.Close();
        //}

        //public void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        //{
        //    var message = messageQueue.EndReceive(e.AsyncResult);
        //    string token = message.Body.ToString();
        //    string Subject = "FundoNotes Claim token";
        //    string Body = token;
        //    string jwt = DecodeJwt(token);
        //    var smtpClient = new SmtpClient("smtp.gmail.com")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential("rish0498@gmail.com", "Rishu@123"),//give dummy gmail
        //        EnableSsl = true,

        //    };

        //    smtpClient.Send("jamadarroopa256@gmail.com", jwt, Subject, Body);
        //    messageQueue.BeginReceive();

        //}
        //public string DecodeJwt(string token)
        //{
        //    try
        //    {
        //        var decodeToken = token;
        //        var handler = new JwtSecurityTokenHandler();
        //        var jsonToken = handler.ReadJwtToken((decodeToken));
        //        var result = jsonToken.Claims.FirstOrDefault().Value;
        //        return result;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
    }
