using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Models
{
    public class Trail
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public byte[] Photo { get; set; }
        public byte[] Map { get; set; }
        public string Author { get; set; }
        public string DifficultyLevel { get; set; }
        public string Location { get; set; }

        public Trail(string Title, double Rating, string Distance, string Duration,
            byte[] Photo, byte[]? Map, string Author, string DifficultyLevel, string Location, string Link)
        {
            this.Title = Title;
            this.Rating = Rating;
            this.Distance = Distance;
            this.Duration = Duration;
            this.Photo = Photo;
            this.Map = Map;
            this.Author = Author;
            this.DifficultyLevel = DifficultyLevel;
            this.Location = Location;
            this.Link = Link;
        }
        public Trail() { }
    }
}
   
