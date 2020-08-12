using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Interfaces
{
    public interface IDataBaseService
    {
        public void LoadToDatabase(List<DetailedTrail> trails);
        public List<DetailedTrail> GetListOfTrails(int page);
        public List<DetailedTrail> GetListOfTrailsForUser(int page);
        public DetailedTrail ReturnSingleTrailFromDb(int id);
        public int GetAmountOfSites();
        public void DeleteRecord(int id);
    }
}
