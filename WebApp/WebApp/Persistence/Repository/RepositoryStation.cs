using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RepositoryStation : Repository<Station, int>, IRepositoryStation
    {
        public RepositoryStation(DbContext context) : base(context)
        {
        }
    }
}