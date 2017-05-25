﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnJapanese.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.DBModule db = new Models.DBModule();

            //initial set up of images and quiz scores
            db.saveImagesAsync();
            db.saveQuiz();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult SelectLesson()
        {
            return RedirectToAction("Lesson/SelectLesson", "LessonController");
        }

    }
}