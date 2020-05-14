using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using nonogram.Core;
using nonogram.Database;
using nonogram.Entity;

namespace NonogramWeb.Models
{
    public class ServiceModel
    {
        public List <string> ImageNameList { get; set; }

        public IList<Score> Scores { get; set; }

        public IList<Comment> Comments { get; set; }

        public int[] Ratings = new int[5];

        public Comment Comment { get; set; }

        public int Rating { get; set; }
    }
}
