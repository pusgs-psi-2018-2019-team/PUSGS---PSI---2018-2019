using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Models;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        [Dependency]
        public IRepositoryDayType RepositoryDayTypes { get; set; }

        [Dependency]
        public IRepositoryLine RepositoryLines { get; set; }

        [Dependency]
        public IRepositoryPricelist RepositoryPricelists { get; set; }

        [Dependency]
        public IRepositoryStation RepositoryStations { get; set; }

        [Dependency]
        public IRepositoryTicketPrice RepositoryTicketPrices { get; set; }

        [Dependency]
        public IRepositoryTicketType RepositoryTicketTypes { get; set; }

        [Dependency]
        public IRepositoryTimetable RepositoryTimetables { get; set; }

        [Dependency]
        public IRepositoryTimetableType RepositoryTimetableTypes { get; set; }

        [Dependency]
        public IRepositoryUserType RepositoryUserTypes { get; set; }

        [Dependency]
        public IRepositoryVehicle RepositoryVehicles { get; set; }
        [Dependency]
        public IRepositoryTicket RepositoryTicket { get; set; }

		public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}