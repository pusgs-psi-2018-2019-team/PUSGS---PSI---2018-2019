using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RepositoryTimetable : Repository<Timetable, int>, IRepositoryTimetable
    {
        public RepositoryTimetable(DbContext context) : base(context)
        {
        }
    }
}