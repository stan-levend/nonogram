using System;

namespace nonogram.Entity
{
    [Serializable]
    public class Score
    {
        public int Id { get; set; }

        public string ImageName { get; set; }

        public string Player { get; set; }

        public int Points { get; set; }
    }
}
