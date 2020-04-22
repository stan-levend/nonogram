using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nonogram.Entity;
using nonogram.Service;

namespace NonogramWeb.APIControllers
{
    [Produces("application/json")]
    [Route("api/Rating")]
    public class RatingController : Controller
    {
        private IRatingService ratingService= new RatingServiceEF();

        // GET: api/Rating
        [HttpGet]
        public IEnumerable<Rating> Get()
        {
            return ratingService.GetLatestRating();
        }

        // POST: api/Rating
        [HttpPost]
        public void Post([FromBody]Rating rating)
        {
            ratingService.AddRating(rating);
        }
    }
}