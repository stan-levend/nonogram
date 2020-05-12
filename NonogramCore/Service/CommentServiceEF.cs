using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using nonogram.Entity;

namespace nonogram.Service
{
    public class CommentServiceEF : ICommentService
    {
        public void AddComment(Comment comment)
        {
            using (var context = new NonogramDbContext())
            {
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }

        public IList<Comment> GetLatestComments()
        {
            using (var context = new NonogramDbContext())
            {
                return (from c in context.Comments
                        orderby c.ID
                        descending
                        select c).Take(20).ToList();
            }
        }

        public void ClearComments()
        {
            using (var context = new NonogramDbContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Comments");
            }
        }
    }
}
