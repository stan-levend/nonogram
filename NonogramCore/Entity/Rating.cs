using System;

namespace nonogram.Entity
{
    [Serializable]
    public class Rating
    {
        public int ID { get; set; }
        
        public int Value { get; set; }

        public string Player { get; set; }
    }
}
