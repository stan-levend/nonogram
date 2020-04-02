using System;

namespace nonogram.Entity
{
    [Serializable]
    public class Rating
    {
        public int RatingID { get; set; }
        
        public int Value { get; set; }

        public string Player { get; set; }
    }
}
