using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using nonogram.Entity;

namespace nonogram.Service
{
    public class RatingServiceFile : IRatingService
    {
        private const string FileName = "rating.bin";

        private List<Rating> rating = new List<Rating>();
        public void AddRating(Rating rating)
        {
            if (rating == null) throw new RatingException("Rating must be not null!");
            if (rating.Value < 1 || rating.Value > 5) throw new RatingException("You can rate this game only by 1-5 stars.");
            //rating.ID = this.rating.Count() + 1;
            this.rating.Add(rating);
            SaveRating();
        }

        public IList<Rating> GetLatestRating()
        {
            LoadRating();
            return (from r in rating
                    orderby r.ID
                    descending select r).Take(10).ToList();
        }

        public int GetAllTimeRating(int stars)
        {
            LoadRating();
            
         /* int[] array = new int[5]; 
            foreach (var r in rating)
            {
                if(r.Value == 1) array[1]++;
                else if(r.Value == 2) array[2]++;
                else if(r.Value == 3) array[3]++;
                else if(r.Value == 4) array[4]++;
                else if(r.Value == 5) array[5]++;
                return array.ToList();
            } */

            return (from r in rating
                    where r.Value == stars
                    select r).Count();
        }

        public void ClearRating()
        {
            rating.Clear();
            File.Delete(FileName);
        }


        private void SaveRating()
        {
            using (var fs = File.OpenWrite(FileName))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, rating);
            }
        }

        private void LoadRating()
        {
            if (File.Exists(FileName))
            {
                using (var fs = File.OpenRead(FileName))
                {
                    var bf = new BinaryFormatter();
                    rating = (List<Rating>)bf.Deserialize(fs);
                }
            }
        }

    }
}
