using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using nonogram.Entity;

namespace nonogram.Service
{
    public class RatingServiceEF : IRatingService
    {
        public void AddRating(Rating rating)
        {
            using (var context = new NonogramDbContext())
            {
                context.Ratings.Add(rating);
                context.SaveChanges();
            }
        }

        public IList<Rating> GetLatestRating()
        {
            using (var context = new NonogramDbContext())
            {
                return (from r in context.Ratings
                        orderby r.ID
                        descending
                        select r).Take(10).ToList();
            }
        }

        public int GetAllTimeRating(int stars)
        {
            using (var context = new NonogramDbContext())
            {
                return (from r in context.Ratings
                        where r.Value == stars
                        select r).Count();
            }
        }

        public void ClearRating()
        {
            using (var context = new NonogramDbContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Ratings");
            }
        }
    }
}
