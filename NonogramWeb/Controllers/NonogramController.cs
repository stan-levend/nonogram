using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using nonogram.Core;
using nonogram.Database;
using nonogram.Entity;
using nonogram.Service;
using NonogramWeb.Models;

namespace NonogramWeb.Controllers
{
    public class NonogramController : Controller
    {
        //IScoreService _scoreService = new ScoreServiceEF();
        //private NonogramModel model = new NonogramModel();

        /*public IActionResult Index()
        {
            return View();
        }
        public IActionResult Image(Difficulty difficulty)
        {
            var model = PrepareModel("Difficulty chosen:", difficulty);
            return View(model);
        }
        public IActionResult PlayingGrid()
        {

            var grid = new Grid(new Dog().Data);
            HttpContext.Session.SetObject("grid", grid);

            var model = PrepareModel("Chosen image");
            return View(model);
        }
        public IActionResult HowToPlay()
        {
            return View();

        }
        public IActionResult HighScore()
        {
            return View();

        }
        public IActionResult CommentRating()
        {
            return View();

        }
        public NonogramModel PrepareModel(string message, Difficulty difficulty)
        {
            return new NonogramModel
            {
                Message = message,
                Difficulty = difficulty,
                DatabaseHandler = new DatabaseHandler(difficulty)
            };
        }
        public NonogramModel PrepareModel(string message)
        {
            return new NonogramModel
            {
                Grid = (Grid)HttpContext.Session.GetObject("grid"),
                Message = message,
            };
        } */
        /*
        public IActionResult PlayingGrid(Difficulty difficulty, string s)
        {

            PrepareModel("Chosen image", difficulty);
            foreach (var item in model.DatabaseHandler.List)
            {
                if (item.Name == s) model.Image = item;   
            }
            var grid = new Grid(model.Image.Data);
            HttpContext.Session.SetObject("grid", grid);

            model.Grid = (Grid)HttpContext.Session.GetObject("grid");
            //PrepareModel("Chosen image", difficulty);
            return View(model);
        }
        public IActionResult Image(Difficulty difficulty)
        {
            PrepareModel("Difficulty chosen:", difficulty);
            return View(model);
        }
        public IActionResult HowToPlay()
        {
            return View();

        }
        
        public IActionResult HighScore()
        {
            return View();
        }
        public IActionResult CommentRating()
        {
            return View();

        }
        public IActionResult Index()
        {
            model.Image = null;
            PrepareModel("New field created");
            return View();

        }
        public void PrepareModel(string message)
        {
            model.Message = message;
        }

        public void PrepareModel(string message, Difficulty difficulty)
        {
            //model.Grid = (Grid)HttpContext.Session.GetObject("grid");
            model.Message = message;
            model.Difficulty = difficulty;
            model.DatabaseHandler = new DatabaseHandler(difficulty);
        } */


        public IActionResult Index()
        {
            var grid = new Grid(new Snail().Data);
            HttpContext.Session.SetObject("grid", grid);

            var model = PrepareModel("Grid prepared");
            return View(model);
        }
        public IActionResult HowToPlay()
        {
            return View();
        }
        public IActionResult HighScore()
        {
            return View();
        }
        public IActionResult CommentRating()
        {
            return View();
        }
        private NonogramModel PrepareModel(string message)
        {
            return new NonogramModel
            {
                Grid = (Grid)HttpContext.Session.GetObject("grid"),
                Message = message,
            };
        }

    }
} 