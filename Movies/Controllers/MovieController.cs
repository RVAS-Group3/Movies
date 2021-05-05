using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    public class MovieController : Controller
    {
        // GET: MovieController
        public ActionResult Index()
            
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.MoviesCollection =
                Models.MongoHelper.database.GetCollection<Models.Movie>("movies");
            var filter = Builders<Models.Movie>.Filter.Ne("","");
            var result = Models.MongoHelper.MoviesCollection.Find(filter).ToList();

            return View(result);
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

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


        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
               //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();  
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
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
