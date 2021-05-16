using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using MongoDB.Driver;
using Movies.Models;

namespace Movies.Controllers
{
    public class RegisterController : Controller
    {

        public ActionResult Index()
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.UserRegistrationCollection =
                Models.MongoHelper.database.GetCollection<Models.UserRegistration>("userReg");
            var filter = Builders<Models.UserRegistration>.Filter.Ne("","");
            List<UserRegistration> result = Models.MongoHelper.UserRegistrationCollection.Find(filter).ToList();

            return View(result);
            //return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.UserRegistrationCollection =
                    Models.MongoHelper.database.GetCollection<Models.UserRegistration>("userReg");

                  //create some _id 
                Object id = GenerateRandomID(24);

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

    }
}
