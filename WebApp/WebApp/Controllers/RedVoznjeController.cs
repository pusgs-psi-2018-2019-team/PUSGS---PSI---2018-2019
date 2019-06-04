using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
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

		// GET: api/RedVoznje
		[ResponseType(typeof(string))]
		public IHttpActionResult GetTimetableTimes(int id) //vraca vremena polaska autobusa iz reda voznji
		{
			string polasci = db.RepositoryTimetables.Get(id).Times;
			if (polasci == null)
			{
				return NotFound();
			}

			return Ok(polasci);
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