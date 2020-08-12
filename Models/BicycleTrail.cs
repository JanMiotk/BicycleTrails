using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Models
{
    public class BicycleTrail : Trail
    {
        public string Trail { get; set; }

        public BicycleTrail(string Title, double Rating, string Distance, string Duration, byte[]? Photo, byte[]? Map,
            string Author, string DifficultyLevel, string? Location, string Link, string Trail) : base(Title, Rating,
                Distance, Duration, Photo, Map, Author, DifficultyLevel, Location, Link)
        {
            this.Trail = Trail;
        }
        public BicycleTrail() { }
    }
}
