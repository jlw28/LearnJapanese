using System;
using System.IO;
using System.Web;
using MongoDB.Driver;
using Newtonsoft.Json;
using MongoDB.Bson;
using System.Collections.Generic;

namespace LearnJapanese.Models
{
    public class DBModule
    {
        /// <summary>
        ///Module for connecting to MongoDB
        /// </summary>

        const string connectionString = "mongodb://localhost";
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public DBModule()
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("Japanese");

        }

        //POST: saves image set information
        public async System.Threading.Tasks.Task saveImagesAsync()
        {
            var file = HttpContext.Current.Server.MapPath(@"~/Content/imagedata.json");
            //string filename = "/Content/imagedata.json";
            var img = File.ReadAllText(file);
            dynamic array = JsonConvert.DeserializeObject(img);

            foreach (var item in array)
            {
                //deserialize again?
                Console.WriteLine(item);
                var document = new BsonDocument
                {
                    {"name", item.name },
                    {"number", item.number},
                    {"set", item.set}
                };
                try
                {
                    var collection = _database.GetCollection<BsonDocument>("images");
                    await collection.InsertOneAsync(document);
                }
                catch
                {
                    Console.WriteLine("Error in saving images");
                }


            }

        }

        //POST: saves quiz set information
        public async System.Threading.Tasks.Task saveQuiz()
        {
            var file = HttpContext.Current.Server.MapPath(@"~/Content/quizscore.json");
            var img = File.ReadAllText(file);
            dynamic array = JsonConvert.DeserializeObject(img);

            foreach (var item in array)
            {
                var document = new BsonDocument
                {
                    {"set", item.set },
                    {"score", item.score},
                    {"date", item.date}
                };
                try
                {
                    var collection = _database.GetCollection<BsonDocument>("quiz");
                    await collection.InsertOneAsync(document);
                }
                catch
                {
                    Console.WriteLine("Error in saving quiz scores");
                }


            }

        }

        //GET: gets image set based on set number
        public List<BsonDocument> getImages(string number)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>("images");
                var filter = Builders<BsonDocument>.Filter.Eq("number", number);
                var results = collection.Find(filter).ToList();
                return results;
            }
            catch
            {
                Console.WriteLine("Error in getting images");
                return null;
            }
        }

        //GET: gets set name and number
        public List<BsonDocument> getSetName()
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>("images");
                var filter = new BsonDocument();
                var fields = Builders<BsonDocument>.Projection.Include("set").Include("number");
                var results = collection.Find(filter).Project<BsonDocument>(fields).ToList();
                return results;
            }
            catch
            {
                Console.WriteLine("Error in getting set names");
                return null;
            }
        }
        //UPDATE: updates quiz score and date 
        public async System.Threading.Tasks.Task updateQuiz(string set, string score, DateTime date)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>("quiz");
                var filter = Builders<BsonDocument>.Filter.Eq("set", set);
                var update = Builders<BsonDocument>.Update
                    .Set("score", score)
                    .Set("date", date);

                var result = await collection.UpdateManyAsync(filter, update);
            }
            catch
            {
                Console.WriteLine("Error in updating quiz score");
            }
    
        }
    }

    public class Image
    {
        public string name { set; get;}
        public string number { set; get; }
        public string set { set; get; }
        public string type { set; get; }
    }

    public class Test
    {
        public string set { set; get; }
        public string score { set; get; }
        public DateTime date { set; get; }
    }
}
