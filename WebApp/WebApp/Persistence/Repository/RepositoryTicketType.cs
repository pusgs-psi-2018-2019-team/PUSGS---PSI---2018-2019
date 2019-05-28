using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RepositoryTicketType : Repository<TicketType, int>, IRepositoryTicketType
    {
        public RepositoryTicketType(DbContext context) : base(context)
        {
        }
    }
}