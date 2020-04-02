using System;

namespace nonogram.Entity
{
    [Serializable]
    public class Comment
    {
        public int CommentID { get; set; }
        
        public string ImageName { get; set; }

        public string Player { get; set; }

        public string Text { get; set; }
    }
}
