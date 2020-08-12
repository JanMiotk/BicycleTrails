using Microsoft.EntityFrameworkCore;
using Models;
using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage
{
    public class DataBaseService : IDataBaseService
    {
        private DataBase _dataBase;

        public DataBaseService()
        {
            _dataBase = new DataBase();
        }
        public void LoadToDatabase(List<DetailedTrail> trails)
        {
            foreach (var record in trails)
            {
                if (_dataBase.Trails.FirstOrDefault(x => x.Title == record.Title) == null)
                    _dataBase.Trails.Add(record);
            }
            _dataBase.SaveChanges();
        }

        public List<DetailedTrail> GetListOfTrails(int page)
        {
            return _dataBase.Trails.Skip(10 * page).Take(10).ToList();
        }

        public List<DetailedTrail> GetListOfTrailsForUser(int page)
        {
            return _dataBase.Trails.Skip(5 * page).Take(5).ToList();
        }

        public DetailedTrail ReturnSingleTrailFromDb(int id)
        {
            return _dataBase.Trails.FirstOrDefault(x => x.Id == id);
        }

        public int GetAmountOfSites()
        {
            return _dataBase.Trails.Count();
        }

        public void DeleteRecord(int id)
        {
            var record = _dataBase.Trails.FirstOrDefault(x => x.Id == id);
            _dataBase.Trails.Remove(record);
            _dataBase.SaveChanges();
        }

    }
}
