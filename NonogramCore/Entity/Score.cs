using System;

namespace nonogram.Entity
{
    [Serializable]
    public class Score
    {
        public string ImageName { get; set; }

        public string Player { get; set; }

        public int Points { get; set; }
    }
}
