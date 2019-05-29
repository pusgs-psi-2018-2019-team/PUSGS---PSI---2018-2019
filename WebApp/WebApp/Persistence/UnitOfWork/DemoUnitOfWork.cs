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
        public List<DayType> DayType { get; set; }
        [Dependency]
        public List<Line> Line { get; set; }
        [Dependency]
        public List<Pricelist> Pricelist { get; set; }
        [Dependency]
        public List<Station> Station { get; set; }
        [Dependency]
        public List<Ticket> Ticket { get; set; }
        [Dependency]
        public List<TicketPrice> TicketPrice { get; set; }
        [Dependency]
        public List<TicketType> TicketType { get; set; }
        [Dependency]
        public List<Timetable> Timetable { get; set; }
        [Dependency]
        public List<TimetableType> TimetableType { get; set; }
        [Dependency]
        public List<UserType> UserType { get; set; }
        [Dependency]
        public List<Vehicle> Vehicle { get; set; }

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