using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    public class TimetableEditController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IUnitOfWork Db { get; set; }

        public TimetableEditController() { }

        public TimetableEditController(IUnitOfWork db)
        {
            this.Db = db;
        }

        [Route("api/timetable/timetableEditGetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var timetables = Db.RepositoryTimetables.GetAll();

            return Ok(timetables);
        }

        [HttpPost]
        [Route("api/timetable/AddTimetable")]
        public IHttpActionResult AddTimetable(Timetable timetable)
        {

            timetable.Line = Db.RepositoryLines.Find(x => x.SerialNumber == timetable.LineId).FirstOrDefault();
            timetable.DayType = Db.RepositoryDayTypes.Find(x => x.Id == timetable.DayTypeId).FirstOrDefault();
            timetable.TimetableType = Db.RepositoryTimetableTypes.Find(x => x.Id == timetable.TimetableTypeId).FirstOrDefault();

            Db.RepositoryTimetables.Add(timetable);
            Db.Complete();
            return Ok("uspesno");
        }

        [HttpPost]
        [Route("api/timetable/UpdateTimetable")]
        public IHttpActionResult UpdateTimetable(Timetable timetable)
        {
            timetable.Line = Db.RepositoryLines.Find(x => x.SerialNumber == timetable.LineId).FirstOrDefault();
            timetable.DayType = Db.RepositoryDayTypes.Find(x => x.Id == timetable.DayTypeId).FirstOrDefault();
            timetable.TimetableType = Db.RepositoryTimetableTypes.Find(x => x.Id == timetable.TimetableTypeId).FirstOrDefault();

            db.Entry(timetable).State = EntityState.Modified;

			try
			{
                db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException e)
			{
				return StatusCode(HttpStatusCode.BadRequest);
			}

            return Ok("uspesno");
        }

        [HttpPost]
        [Route("api/timetable/DeleteTimetable")]
        public IHttpActionResult DeleteTimetable(Timetable timetable)
        {
            timetable.Line = Db.RepositoryLines.Find(x => x.SerialNumber == timetable.LineId).FirstOrDefault();
            timetable.DayType = Db.RepositoryDayTypes.Find(x => x.Id == timetable.DayTypeId).FirstOrDefault();
            timetable.TimetableType = Db.RepositoryTimetableTypes.Find(x => x.Id == timetable.TimetableTypeId).FirstOrDefault();

            db.Entry(timetable).State = EntityState.Deleted;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            return Ok("uspesno");
        }
    }
}
