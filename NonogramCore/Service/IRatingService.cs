using System.Collections.Generic;
using nonogram.Entity;

namespace nonogram.Service
{
    public interface IRatingService
    {
        void AddRating(Rating rating);

        IList<Rating> GetLatestRating();

        int GetAllTimeRating(int stars);

        void ClearRating();
    }
}
