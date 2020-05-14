using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nonogram.Core;
using nonogram.Database;
using nonogram.Entity;

namespace NonogramWeb.Models
{
    public class NonogramModel
    {
        public Grid Grid { get; set; }

        public Legend Legend { get; set; }

        public Image Image { get; set; }

        public string Message { get; set; }

        public List<string> ImageNameList { get; set; }

        public Difficulty Difficulty { get; set; }

    }
}
