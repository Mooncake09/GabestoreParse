using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using System.ComponentModel.DataAnnotations;

namespace GabestoreParse
{
    public class Game
    {
        public Game(
            string title,
            float price,
            string genre,
            string platform,
            DateTime releaseDate,
            string publisher,
            string developer)
        {
            Title = title;

            if (price > 0)
            {
                Price = price;
            } 
            else 
            {
                Price = 50f;
            } 

            Genre = genre;
            Platform = platform;
            ReleaseDate = releaseDate;
            Publisher = publisher;
            Developer = developer;
        }

        public Game() { }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
    }
}
