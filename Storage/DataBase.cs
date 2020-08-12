using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Storage
{
    public class DataBase : DbContext
    {
        private readonly string _path;
        public DbSet<DetailedTrail> Trails { get; set; }

        public DataBase(DbContextOptions<DataBase> options)
        : base(options) { }

        public DataBase()
        {
            using(var file = new StreamReader("appsettings.json"))
            {
                var json = file.ReadToEnd();
                var config = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
                _path = config["ConnectionStrings"]["Msql"];
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_path);
        }

    }
}
