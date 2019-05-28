using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RepositoryVehicle : Repository<Vehicle, int>, IRepositoryVehicle
    {
        public RepositoryVehicle(DbContext context) : base(context)
        {
        }
    }
}