using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Models.BindingModels;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    public class LineEditController : ApiController
    {
		private ApplicationDbContext db = new ApplicationDbContext();
		public IUnitOfWork Db { get; set; }

		private const string LocalLoginProvider = "Local";
		private ApplicationUserManager _userManager;

		public LineEditController() { }

		public LineEditController(IUnitOfWork db)
		{
			this.Db = db;
		}

		// GET: api/LineEdit/Lines
		[Authorize(Roles = "Admin")]
		[ResponseType(typeof(List<int>))]
		[Route("api/LineEdit/Lines")]
		public IHttpActionResult GetLines()
		{
			List<int> ret = new List<int>();
			List<Line> line = new List<Line>();

			line = Db.RepositoryLines.GetAll().ToList();

			foreach(Line l in line)
			{
				ret.Add(l.SerialNumber);
			}

			return Ok(ret);
		}

		// GET: api/LineEdit/SelectedLine
		[Authorize(Roles = "Admin")]
		[ResponseType(typeof(Line))]
		[Route("api/LineEdit/SelectedLine/{serial}")]
		public IHttpActionResult GetSelectedLine(string serial)
		{
			List<Line> line = new List<Line>();
			Line ret = new Line();
			line = Db.RepositoryLines.GetAll().ToList();
			int serialNumber = Int32.Parse(serial);

			foreach (Line l in line)
			{
				if(l.SerialNumber.Equals(serialNumber))
				{
					ret = l;
					break;
				}
			}

			if(ret != null)
				return Ok(ret);
			else
				return StatusCode(HttpStatusCode.BadRequest);

		}

		// GET: api/LineEdit/SelectedLine
		[Authorize(Roles = "Admin")]
		[ResponseType(typeof(List<string>))]
		[Route("api/LineEdit/GetStations/{serial}")]
		public IHttpActionResult GetStations(string serial)
		{
			List<Line> line = new List<Line>();
			Line ret = new Line();
			line = Db.RepositoryLines.GetAll().ToList();
			int serialNumber = Int32.Parse(serial);

			foreach (Line l in line)
			{
				if (l.SerialNumber.Equals(serialNumber))
				{
					ret = l;
					break;
				}
			}
			
			List<Station> list = Db.RepositoryStations.Find(x => x.Lines.All(y => y.Id.Equals(ret.Id))).ToList();

			List<string> returnsList = new List<string>();

			foreach(Station s in list)
			{
				returnsList.Add(s.Name);
			}

			if (returnsList != null)
				return Ok(returnsList);
			else
				return StatusCode(HttpStatusCode.BadRequest);

		}

		// GET: api/LineEdit/SelectedLine
		[Authorize(Roles = "Admin")]
		[ResponseType(typeof(Station))]
		[Route("api/LineEdit/GetSelectedStation/{name}")]
		public IHttpActionResult GetSelectedStation(string name)
		{
			List<Station> stations = new List<Station>();
			Station ret = new Station();
			stations = Db.RepositoryStations.GetAll().ToList();

			foreach (Station l in stations)
			{
				if (l.Name.Equals(name))
				{
					ret = l;
					break;
				}
			}

			if (ret != null)
				return Ok(ret);
			else
				return StatusCode(HttpStatusCode.BadRequest);

		}

		// DELETE: api/LineEdit/DeleteSelectedLine/{serial}
		[Authorize(Roles = "Admin")]
		[ResponseType(typeof(string))]
		[Route("api/LineEdit/DeleteSelectedLine/{serial}")]
		public IHttpActionResult DeleteSelectedLine(string serial)
		{
			List<Line> line = new List<Line>();
			Line ret = new Line();
			line = Db.RepositoryLines.GetAll().ToList();
			int serialNumber = Int32.Parse(serial);

			foreach (Line l in line)
			{
				if (l.SerialNumber.Equals(serialNumber))
				{
					ret = l;
					break;
				}
			}

			if (ret != null)
			{

				db.Entry(ret).State = EntityState.Deleted;

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
			else
				return StatusCode(HttpStatusCode.BadRequest);

		}

		// POST: api/LineEdit/AddLine
		[ResponseType(typeof(string))]
		[Route("api/LineEdit/AddLine")]
		[Authorize(Roles = "Admin")]
		public IHttpActionResult AddLine(AddLine line)
		{
			bool postoji = false;
			List<Line> provera = new List<Line>();
			provera = Db.RepositoryLines.GetAll().ToList();

			foreach(Line l in provera)
			{
				if(l.SerialNumber.Equals(line.SerialNumber))
				{
					postoji = true;
					break;
				}
			}

			if(!postoji)
			{
				Line ret = new Line();
				ret.SerialNumber = line.SerialNumber;

				List<Station> temp = new List<Station>();
				temp = Db.RepositoryStations.GetAll().ToList();
				List<Station> stations = new List<Station>();

				foreach (Station s in temp)
				{
					if (line.StationsAdd.Contains(s.Name))
					{
						stations.Add(s);
					}
				}

				ret.Stations = stations;
				
				Db.RepositoryLines.Add(ret);
				Db.Complete();

				return Ok("uspesno");
			}
			else
			{
				return StatusCode(HttpStatusCode.BadRequest);
			}
		}

		// GET: api/LineEdit/GetAllStations
		[Authorize(Roles = "Admin")]
		[ResponseType(typeof(List<string>))]
		[Route("api/LineEdit/GetAllStations")]
		public IHttpActionResult GetAllStations()
		{
			List<Station> stations = new List<Station>();
			List<string> ret = new List<string>();

			stations = Db.RepositoryStations.GetAll().ToList();

			foreach (Station l in stations)
			{
				ret.Add(l.Name);
			}

			if (ret != null)
				return Ok(ret);
			else
				return StatusCode(HttpStatusCode.BadRequest);

		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
