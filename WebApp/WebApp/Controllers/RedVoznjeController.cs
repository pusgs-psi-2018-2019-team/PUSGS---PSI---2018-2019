using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Models.BindingModels;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
	[Authorize]
	[RoutePrefix("api/RedVoznje")]
	public class RedVoznjeController : ApiController
	{
		private IUnitOfWork db;

		private const string LocalLoginProvider = "Local";
		private ApplicationUserManager _userManager;

		public RedVoznjeController() { }

		public RedVoznjeController(IUnitOfWork db)
		{
			this.db = db;
		}

		/*public RedVoznjeController(ApplicationUserManager userManager,
			ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
		{
			UserManager = userManager;
			AccessTokenFormat = accessTokenFormat;
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }*/

		// GET: api/RedVoznje/IspisReda/{timetableTypeId}/{dayTypeId}/{lineId}
		//[ResponseType(typeof(string))]
		[Route("IspisReda/{timetableTypeId}/{dayTypeId}/{lineId}")]
		[HttpGet]
		public IHttpActionResult GetTimetableTimes(int timetableTypeId,int dayTypeId,int lineId) //vraca vremena polaska autobusa iz reda voznji
		{
			Timetable t = new Timetable();
			t = db.RepositoryTimetables.Find(x => x.TimetableTypeId == timetableTypeId && x.DayTypeId == dayTypeId && x.LineId == lineId).FirstOrDefault();

			return Ok(t.Times);
		}

		// GET: api/RedVoznje/RedVoznjiInfo
		[ResponseType(typeof(RedVoznjeInfoBindingModel))]
		[Route("RedVoznjiInfo")]
		public IHttpActionResult GetScheduleInfo()
		{
			List<TimetableType> timetableTypes = db.RepositoryTimetableTypes.GetAll().ToList();
			List<Line> lines = db.RepositoryLines.GetAll().ToList();
			List<DayType> dayTypes = db.RepositoryDayTypes.GetAll().ToList();
			RedVoznjeInfoBindingModel s = new RedVoznjeInfoBindingModel() { TimetableTypes = timetableTypes, Lines = lines, DayTypes = dayTypes };

			return Ok(s);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool TimetableExist(int id)
		{
			return db.RepositoryTimetables.GetAll().Count(e => e.Id == id) > 0;
		}

	}
}