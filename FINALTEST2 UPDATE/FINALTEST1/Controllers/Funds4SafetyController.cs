using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FINALTEST1.Data;
using FINALTEST1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System.Text;
using NToastNotify;

namespace FINALTEST1.Controllers
{
    public class Funds4SafetyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SMTPConfig _smtpConfig;
        private readonly IToastNotification _toastNotification;

        public Funds4SafetyController(ApplicationDbContext context, IOptions<SMTPConfig> smtpConfig, IToastNotification toastNotification)
        {
            _context = context;
            _smtpConfig = smtpConfig.Value;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            decimal totalAmount = _context.Monetaries
                .Where(a => a.Validate == true)
                .ToList().Sum(a => a.Amount);
            return View(totalAmount);
        }

        public IActionResult Donate()
        {
            return View();
        }

        public IActionResult Donators()
        {
            return View();
        }

        public IActionResult Proof()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        private int UserID;

        [HttpPost]
        public IActionResult Donate(UserMonetary userMonetary, IFormFile Image)
        {
            var newUser = new User()
            {
                FirstName = userMonetary.User.FirstName.TrimStart().TrimEnd(),
                LastName = userMonetary.User.LastName.TrimStart().TrimEnd(),
                City = userMonetary.User.City.TrimStart().TrimEnd(),
                Email = userMonetary.User.Email.TrimStart().TrimEnd()
            };
            _toastNotification.AddInfoToastMessage(newUser.LastName + " " + newUser.FirstName + " Thank you for your donation!");
            UserID = GetUserId(newUser);
            if (UserID != 0)
            {
                AddNewMonetary(userMonetary, Image);
                SendEmail(newUser.Email);
                return RedirectToAction("Donate");
            }
            else
            {
                _context.Users.Add(newUser);
                _context.SaveChanges();
                UserID = GetUserId(newUser);
                AddNewMonetary(userMonetary, Image);
                SendEmail(newUser.Email);
                return RedirectToAction("Donate");
            }
        }

        private int GetUserId(User user)
        {
            var User = from u in _context.Users
                       where u.FirstName == user.FirstName && 
                       u.LastName == user.LastName && 
                       u.City == user.City && 
                       u.Email == user.Email
                       select new { u.UserID };
            if (User.FirstOrDefault() == null)
                return 0;
            else
                return User.First().UserID;
        }

        private void AddNewMonetary(UserMonetary userMonetary, IFormFile Image)
        {
            Random rand = new Random();
            int randomNum = rand.Next(1, 10000);
            string fileName = randomNum + Image.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Image", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
                Image.CopyTo(stream);
            var newMonetary = new Monetary()
            {
                UserID = UserID,
                Amount = userMonetary.Monetary.Amount,
                Image = fileName,
                Date = DateTime.Now,
                Validate = false
            };
            _context.Monetaries.Add(newMonetary);
            _context.SaveChanges();
        }

        public void SendEmail(string email)
        {
            Email userEmailOptions = new Email
            {
                ToEmail = email,
                Subject = "This is test email from Funds4Safety fundraising website",
                Body = System.IO.File.ReadAllText(string.Format(@"EmailTemplate/EmailContent.html"))
            };
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                IsBodyHtml = _smtpConfig.IsBodyHTML,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                Body = userEmailOptions.Body
            };
            mail.To.Add(userEmailOptions.ToEmail);
            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);
            SmtpClient smtpClient = new SmtpClient
            {
                Port = _smtpConfig.Port,
                Host = _smtpConfig.Host,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mail.BodyEncoding = Encoding.Default;
            smtpClient.Send(mail);
        }
    }
}