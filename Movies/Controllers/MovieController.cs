using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    
    public class MovieController : Controller
    {
        // GET: MovieController
        [Authorize]
        public ActionResult Index()
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.MoviesCollection =
                Models.MongoHelper.database.GetCollection<Models.Movie>("movies");
            var filter = Builders<Models.Movie>.Filter.Ne("","");
            //var filter = Builders<Models.Movie>.Filter.Ne("Id", "");
            var result = Models.MongoHelper.MoviesCollection.Find(filter).ToList();
            return View(result);
        }

        [Authorize]
        // GET: MovieController/Details/5
        public ActionResult Details(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.MoviesCollection =
                Models.MongoHelper.database.GetCollection<Models.Movie>("movies");
            var filter = Builders<Models.Movie>.Filter.Eq("_id", id);
            //FirstOrDefault to get only one result 
            var result = Models.MongoHelper.MoviesCollection.Find(filter).ToList().FirstOrDefault();
            return View(result);
        }
        [Authorize]
        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: MovieController/Create
        [HttpPost]
         
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.MoviesCollection =
                    Models.MongoHelper.database.GetCollection<Models.Movie>("movies");
              

                //create some _id 
                Object id = GenerateRandomID(24);

                Models.MongoHelper.MoviesCollection.InsertOneAsync(new Models.Movie
                {
                    _id = id,
                    movieTitle = collection["movieTitle"],
                    releaseYear = collection["releaseYear"],
                    director = collection["director"],
                    writers = collection["writers"],
                    stars = collection["stars"],
                    story = collection["story"]

                });
                return RedirectToAction("Index");
                //return RedirectToAction(nameof(Index));


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

        [Authorize]
        // GET: MovieController/Edit/5
        //public ActionResult Edit(int id)
        public ActionResult Edit(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.MoviesCollection =
                Models.MongoHelper.database.GetCollection<Models.Movie>("movies");
            var filter = Builders<Models.Movie>.Filter.Eq("_id", id);
            //FirstOrDefault to get only one result 
            var result = Models.MongoHelper.MoviesCollection.Find(filter).ToList().FirstOrDefault();
            return View(result);
        }
        [Authorize]
        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.MoviesCollection =
                    Models.MongoHelper.database.GetCollection<Models.Movie>("movies");
                var filter = Builders<Models.Movie>.Filter.Eq("_id", id);
                var update = Builders<Models.Movie>.Update
                    .Set("movieTitle", collection["movieTitle"])
                    .Set("releaseYear", collection["releaseYear"])
                    .Set("director", collection["director"])
                    .Set("writers", collection["writers"])
                    .Set("stars", collection["stars"])
                    .Set("story", collection["story"]);
                var result = Models.MongoHelper.MoviesCollection.UpdateOneAsync(filter, update);

                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();  
            }
        }
        [Authorize]
        // GET: MovieController/Delete/5
        public ActionResult Delete(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.MoviesCollection =
                Models.MongoHelper.database.GetCollection<Models.Movie>("movies");
            var filter = Builders<Models.Movie>.Filter.Eq("_id", id);
            //FirstOrDefault to get only one result 
            var result = Models.MongoHelper.MoviesCollection.Find(filter).ToList().FirstOrDefault();
            return View(result);
        }
        [Authorize]
        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.MoviesCollection =
                    Models.MongoHelper.database.GetCollection<Models.Movie>("movies");
                var filter = Builders<Models.Movie>.Filter.Eq("_id", id);
                var result = Models.MongoHelper.MoviesCollection.DeleteOneAsync(filter);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index");
             }
            catch
            {
                return View();
            }
        }
    }
}
