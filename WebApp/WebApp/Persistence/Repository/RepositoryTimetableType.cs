using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RepositoryTimetableType : Repository<TimetableType, int>, IRepositoryTimetableType
    {
        public RepositoryTimetableType(DbContext context) : base(context)
        {
        }
    }
}