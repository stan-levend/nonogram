using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using nonogram.Entity;

namespace nonogram.Service
{
    public class CommentServiceFile : ICommentService
    {
        private const string FileName = "comment.bin";

        private List<Comment> comments = new List<Comment>();
        public void AddComment(Comment comment)
        {
            if (comment == null) throw new CommentException("Comment must be not null!");
            if (comment.Text == null) throw new CommentException("Comment text field can't remain blank.");
            comment.CommentID = comments.Count() + 1;
            
            comments.Add(comment);
            SaveComments();
        }

        public IList<Comment> GetLatestComments()
        {
            LoadComments();
            return (from c in comments
                    orderby c.CommentID
                    descending select c).Take(3).ToList();
        }

        public void ClearComments()
        {
            comments.Clear();
            File.Delete(FileName);
        }


        private void SaveComments()
        {
            using (var fs = File.OpenWrite(FileName))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, comments);
            }
        }

        private void LoadComments()
        {
            if (File.Exists(FileName))
            {
                using (var fs = File.OpenRead(FileName))
                {
                    var bf = new BinaryFormatter();
                    comments = (List<Comment>)bf.Deserialize(fs);
                }
            }
        }

    }
}
