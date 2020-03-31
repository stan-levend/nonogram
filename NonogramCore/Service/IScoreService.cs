using System.Collections.Generic;
using nonogram.Entity;

namespace nonogram.Service
{
    public interface IScoreService
    {
        void AddScore(Score score);

        IList<Score> GetTopScores(string imageName);

        void ClearScores();
    }
}
