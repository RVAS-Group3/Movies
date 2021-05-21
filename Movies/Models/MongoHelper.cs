using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Movies.Models
{
    public class MongoHelper
    {
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }

        public static string MongoConnection = "mongodb+srv://rafuser1:Raf2021@cluster1.glwrz.mongodb.net/MoviesDB?retryWrites=true&w=majority";
        public static string MongoDatabase = "MoviesDB";

        public static IMongoCollection<Models.Movie> MoviesCollection { get; set; }


        internal static void ConnectToMongoService()
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
