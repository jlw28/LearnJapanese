using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace LearnJapanese.Controllers
{
    public class QuizController : Controller
    {
        Models.DBModule db = new Models.DBModule();

        //GET: SelectQuiz
        public ActionResult SelectQuiz()
        {
              return View("SelectQuiz");
        }

        //GET: Quiz
        public ActionResult Quiz(string number)
        {
            List<BsonDocument> img = db.getImages(number);



            return View("Quiz");
        }
      
    }
}
