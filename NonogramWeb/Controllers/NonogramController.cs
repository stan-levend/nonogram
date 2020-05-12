using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
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
        private NonogramModel model = new NonogramModel();
        private IScoreService scoreService = new ScoreServiceEF();

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Image(Difficulty difficulty)
        {
            model.Difficulty = difficulty;
            model.ImageNameList= new DatabaseHandler(difficulty).NameList;
            return View(model);
        }
        public IActionResult PlayingGrid(string chosenImageName, Difficulty difficulty)
        {
            var ImageList = new DatabaseHandler(difficulty).List;

            foreach (var image in ImageList)
            {
                if (image.Name == chosenImageName)
                {
                    model.Image = image;
                    HttpContext.Session.SetObject("image", image);
                }
            }

            var grid = new Grid(model.Image.Data);
            HttpContext.Session.SetObject("grid", grid);
            var legend = new Legend(model.Image.Data);
            HttpContext.Session.SetObject("legend", legend);

            PrepareModel("Grid prepared");
            return View(model);
        }
        public IActionResult ChangeState(int x, int y)
        {
            var grid = (Grid)HttpContext.Session.GetObject("grid");
            if (grid.Tiles[y, x].Input == TileState.Colored)
                grid.BlankTile(x, y);
            else grid.MarkTile(x, y);
            HttpContext.Session.SetObject("grid", grid);

            if (GameIsWon(grid))
                PrepareModel("Congratulations, you have won!");
            else PrepareModel($"Changed State of tile ({x+1}, {y+1})");

            return View("PlayingGrid", model);
        }
        public IActionResult RevealHint()
        {
            var grid = (Grid)HttpContext.Session.GetObject("grid");
            if (grid.Hints > 0 && grid.CurrentState == GameState.Playing)
            {
                grid.RevealHint();
                HttpContext.Session.SetObject("grid", grid);

                if (GameIsWon(grid))
                    PrepareModel("Congratulations, you have won!");
                else PrepareModel("Hint revealed!");

            } else PrepareModel("Can't use more hints.");


            return View("PlayingGrid", model);
        }
        public IActionResult SolveGrid()
        {
            var grid = (Grid)HttpContext.Session.GetObject("grid");
            if (grid.CurrentState == GameState.Playing)
            {
                grid.Solve();
                HttpContext.Session.SetObject("grid", grid);
                PrepareModel("Solving grid.");
            }
            else PrepareModel("The grid is already solved");

            return View("PlayingGrid", model);
        }
        public IActionResult ClearGrid()
        {
            var grid = (Grid)HttpContext.Session.GetObject("grid");
            if (grid.CurrentState == GameState.Playing)
            {
                grid.Clear();
                HttpContext.Session.SetObject("grid", grid);
                PrepareModel("You have cleared the grid.");
            }
            else PrepareModel($"The game is already {grid.CurrentState}");

            return View("PlayingGrid", model);
        }

        public IActionResult NewGame()
        {
            var grid = (Grid)HttpContext.Session.GetObject("grid");
            HttpContext.Session.SetObject("grid", new Grid(grid.ChosenImage));
            PrepareModel("New grid prepared");

            return View("PlayingGrid", model);
        }

        private void PrepareModel(string message)
        {
            model.Grid = (Grid)HttpContext.Session.GetObject("grid");
            model.Legend = (Legend)HttpContext.Session.GetObject("legend");
            model.Message = message;
        }

        private bool GameIsWon(Grid grid)
        {
            if (grid.CurrentState == GameState.Won)
            {
                model.Image = (Image)HttpContext.Session.GetObject("image");
                scoreService.AddScore(new Score
                {
                    Player = Environment.UserName,
                    Points = grid.GetScore(),
                    ImageName = model.Image.Name
                });
                return true;
            }
            return false;
        }
    }
} 