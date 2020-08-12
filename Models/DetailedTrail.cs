using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class DetailedTrail : BicycleTrail
    {
        public string KindOfActivity { get; set; }
        public string AverageSpeed { get; set; }
        public string Exceedance { get; set; }
        public string SumUp { get; set; }
        public string SumDown { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }

        public DetailedTrail(string Description, string KindOfActivity, string AverageSpeed, string Exceedance,
            string SumUp, string SumDown, string Data, string Title, double Rating, string Distance, string Duration, byte[]? Photo, byte[]? Map,
            string Author, string DifficultyLevel, string? Location, string Link, string Trails) : base(Title, Rating, Distance, Duration, Photo, Map,
                Author, DifficultyLevel, Location, Link, Trails)
        {
            this.KindOfActivity = KindOfActivity;
            this.AverageSpeed = AverageSpeed;
            this.Exceedance = Exceedance;
            this.SumUp = SumUp;
            this.SumDown = SumDown;
            this.Data = Data;
            this.Description = Description;
        }

        public DetailedTrail() { }
    }
}
