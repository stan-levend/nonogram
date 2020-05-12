using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nonogram.Database;
using nonogram.Entity;
using nonogram.Service;
using NonogramWeb.Models;

namespace NonogramWeb.Controllers
{
    public class ServiceController : Controller
    {
        private ServiceModel model = new ServiceModel();
        private IScoreService scoreService = new ScoreServiceEF();
        private ICommentService commentService = new CommentServiceEF();
        private IRatingService ratingService = new RatingServiceEF();

        public IActionResult HowToPlay()
        {
            return View();
        }
        public IActionResult HighScore()
        {
            model.Scores = scoreService.GetAllScores();
            var combinedNameList = new DatabaseHandler(nonogram.Core.Difficulty.Easy).NameList.Concat
                          (new DatabaseHandler(nonogram.Core.Difficulty.Medium).NameList).Concat
                          (new DatabaseHandler(nonogram.Core.Difficulty.Hard).NameList).ToList();
            model.ImageNameList = combinedNameList;
            return View(model);
        }
        public IActionResult CommentRating()
        {
            PrepareModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddComment(ServiceModel serviceModel)
        {

            if (ModelState.IsValid)
            {
                if (serviceModel.Comment.ImageName == null) serviceModel.Comment.ImageName = "Not Stated";
                commentService.AddComment(new Comment
                {
                    Player = Environment.UserName,
                    Text = serviceModel.Comment.Text,
                    ImageName = serviceModel.Comment.ImageName
                });
            }
            PrepareModel();
            return View("CommentRating", model);
        }
        [HttpPost]
        public IActionResult AddRating(ServiceModel serviceModel)
        {

            ratingService.AddRating(new Rating
            {
                Player = Environment.UserName,
                Value = serviceModel.Rating
            });

            PrepareModel();
            return View("CommentRating", model);
        }

        public void PrepareModel()
        {
            model.Comments = commentService.GetLatestComments();
            for (int i = 1; i <= 5; i++)
                model.Ratings[i - 1] = ratingService.GetAllTimeRating(i);
        }
    }
}