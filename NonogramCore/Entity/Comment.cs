using System;

namespace nonogram.Entity
{
    [Serializable]
    public class Comment
    {
        public int ID { get; set; }
        
        public string ImageName { get; set; }

        public string Player { get; set; }

        public string Text { get; set; }
    }
}
