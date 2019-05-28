using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RepositoryTicket : Repository<Ticket, int>, IRepositoryTicket
    {
        public RepositoryTicket(DbContext context) : base(context)
        {
        }
    }
}