using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnJapanese.Controllers
{
    public class LessonController : Controller
    {
        // GET: SelectLesson
        public ActionResult SelectLesson()
        {
            return View("SelectLesson");
        }

        //GET: Lesson
        public ActionResult Lesson(int id, string name)
        {
            //*****needs to be reworked to query database for images*******
            //Retrieves images based on parameters selected 
            List<Models.Lesson> Set = new List<Models.Lesson>();
            if (id < 14)
            {
                for(int i = 1; i <= 5; i++)
                {
                    var set = new Models.Lesson();
                    set.img = "/Content/Images/" + id.ToString() + "/" + i.ToString() + ".jpg";
                    set.SetNumber = id;
                    set.SetName = name;
                    Set.Add(set);
                }
            }
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    var set = new Models.Lesson();
                    set.img = "/Content/Images/" + id.ToString() + "/" + i.ToString() + ".jpg";
                    set.SetNumber = id;
                    set.SetName = name;
                    Set.Add(set);
                }
            }
       
            return View("Lesson", Set);
        }
    }
}