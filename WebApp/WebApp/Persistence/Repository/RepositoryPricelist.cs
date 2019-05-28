using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RepositoryPricelist : Repository<Pricelist, int>, IRepositoryPricelist
    {
        public RepositoryPricelist(DbContext context) : base(context)
        {
        }
    }
}