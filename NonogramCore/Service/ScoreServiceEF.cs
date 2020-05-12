using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using nonogram.Entity;

namespace nonogram.Service
{
    public class ScoreServiceEF : IScoreService
    {
        public void AddScore(Score score)
        {
            using (var context = new NonogramDbContext())
            {
                context.Scores.Add(score);
                context.SaveChanges();
            }
        }

        public void ClearScores()
        {
            using (var context = new NonogramDbContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Scores");
            }
        }

        public IList<Score> GetTopImageScores(string imageName)
        {
            using (var context = new NonogramDbContext())
            {
                return (from s in context.Scores
                        where s.ImageName == imageName
                        orderby s.Points
                        descending
                        select s).Take(5).ToList();
            }
        }

        public IList<Score> GetAllScores()
        {
            using (var context = new NonogramDbContext())
            {
                return (from s in context.Scores
                        orderby s.ImageName, s.Points
                        descending
                        select s).ToList();
            }
        }
    }
}
