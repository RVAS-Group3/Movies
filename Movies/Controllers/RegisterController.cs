using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using MongoDB.Driver;
using Movies.Models;
using Microsoft.AspNetCore.Authorization;
using System.Web;

namespace Movies.Controllers
{
    public class RegisterController : Controller
    {

        public ActionResult Index()
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.UserRegistrationCollection =
                Models.MongoHelper.database.GetCollection<Models.UserRegistration>("userReg");

            var filter = Builders<Models.UserRegistration>.Filter.Ne("", "");
            var result = Models.MongoHelper.UserRegistrationCollection.Find(filter).ToList();

            return View(result);
            //return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public bool IsEmailExists(string eMail)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.UserRegistrationCollection =
                Models.MongoHelper.database.GetCollection<Models.UserRegistration>("userReg");
            var IsCheck = Builders<Models.UserRegistration>.Filter.Where(email => email.email == eMail); 
            return IsCheck != null;                
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.UserRegistrationCollection =
                    Models.MongoHelper.database.GetCollection<Models.UserRegistration>("userReg");

                  //create some _id 
                Object id = GenerateRandomID(24);

                //check if email already exists

                var userReg = new UserRegistration();
                var IsExists = IsEmailExists(userReg.email);
                if (IsExists)
                {
                    ModelState.AddModelError("EmailExists", "Email already exists");
                   return View("Create");
                }

                //insert new user in database
                Models.MongoHelper.UserRegistrationCollection.InsertOneAsync(new Models.UserRegistration
                {
                    _id = id,
                    firstName = collection["firstName"],
                    lastName = collection["lastName"],
                    email = collection["email"],
                    password = Movies.Models.encryptPassword.textToEncrypt(collection["password"])
                });

                return RedirectToAction("Index");
                //return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }

        private static Random random = new Random();
        private object GenerateRandomID(int v)
        {
            string strarray = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(strarray, v).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool VerifyLogin(string passWord, string eMail)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.UserRegistrationCollection =
        Models.MongoHelper.database.GetCollection<Models.UserRegistration>("userReg");
            var isValidEmail = Builders<Models.UserRegistration>.Filter.Where(x => x.email == eMail);
            var isValidPassword = Builders<Models.UserRegistration>.Filter.Where(y => y.password == passWord);
            return isValidEmail != null && isValidPassword != null;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(IFormCollection collection, UserLogin userLogin)
        {

            var _passWord = Movies.Models.encryptPassword.textToEncrypt(userLogin.password);
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.UserRegistrationCollection =
                Models.MongoHelper.database.GetCollection<Models.UserRegistration>("userReg");
                        
            var isVerify = VerifyLogin(_passWord, userLogin.email);

            if (isVerify)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Information... Please try again!");
            }

            //return RedirectToAction("Index"); 
            return View();
        }

    }
}
