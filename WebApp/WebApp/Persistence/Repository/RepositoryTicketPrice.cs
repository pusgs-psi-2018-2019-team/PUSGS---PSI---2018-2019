using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RepositoryTicketPrice : Repository<TicketPrice, int>, IRepositoryTicketPrice
    {
        public RepositoryTicketPrice(DbContext context) : base(context)
        {
        }
    }
}