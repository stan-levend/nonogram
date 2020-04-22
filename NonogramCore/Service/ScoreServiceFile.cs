﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using nonogram.Entity;

namespace nonogram.Service
{
    public class ScoreServiceFile : IScoreService
    {
        private const string FileName = "score.bin";

        private List<Score> scores = new List<Score>();

        public void AddScore(Score score)
        {
            if (score == null) throw new ScoreException("Score must be not null!");
            if (score.Points < 0) throw new ScoreException("Score cannot be negative value!");

            //score.Id = scores.Count() + 1;
            scores.Add(score);
            SaveScore();
        }

        public IList<Score> GetTopImageScores(string imageName)
        {
            LoadScore();
            return (from s in scores 
                    where s.ImageName == imageName
                    orderby s.Points 
                    descending select s).Take(5).ToList();
        }

        public IList<Score> GetAllScores()
        {
            LoadScore();
            return (from s in scores 
                    orderby s.Points 
                    descending select s).ToList();
        }

        public void ClearScores()
        {
            scores.Clear();
            File.Delete(FileName);
        }


        private void SaveScore()
        {
            using (var fs = File.OpenWrite(FileName))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, scores);
            }
        }

        private void LoadScore()
        {
            if (File.Exists(FileName))
            {
                using (var fs = File.OpenRead(FileName))
                {
                    var bf = new BinaryFormatter();
                    scores = (List<Score>)bf.Deserialize(fs);
                }
            }
        }

    }
}
