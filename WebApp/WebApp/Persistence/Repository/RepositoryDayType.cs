using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RepositoryDayType : Repository<DayType, int>, IRepositoryDayType
    {
        public RepositoryDayType(DbContext context) : base(context)
        {
        }
    }
}