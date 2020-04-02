using System.Collections.Generic;
using nonogram.Entity;

namespace nonogram.Service
{
    public interface ICommentService
    {
        void AddComment(Comment comment);

        IList<Comment> GetLatestComments();

        void ClearComments();
    }
}
