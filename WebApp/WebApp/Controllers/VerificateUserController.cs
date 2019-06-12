using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
	[Authorize]
	public class VerificateUserController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		public IUnitOfWork Db { get; set; }

		private const string LocalLoginProvider = "Local";
		private ApplicationUserManager _userManager;

		public VerificateUserController() { }

		public VerificateUserController(IUnitOfWork db)
		{
			this.Db = db;
		}

		// GET: api/UserVerification/Users
		[AllowAnonymous]
		[ResponseType(typeof(List<ApplicationUser>))]
		[Route("api/UserVerification/Users")]
		public IHttpActionResult GetUser()
		{
			List<ApplicationUser> ret = new List<ApplicationUser>();

			var userStore = new UserStore<ApplicationUser>(db);
			var userManager = new UserManager<ApplicationUser>(userStore);

			List<ApplicationUser> list = userManager.Users.ToList();

			foreach(ApplicationUser a in list)
			{
				if(a.VerificateAcc == 0)
				{
					ret.Add(a);
				}
			}

			return Ok(ret);
		}

		// GET: api/UserVerification/SelectedUser/{username}
		[AllowAnonymous]
		[ResponseType(typeof(ApplicationUser))]
		[Route("api/UserVerification/SelectedUser/{id}")]
		public IHttpActionResult GetSelectedUser(string id)
		{
			ApplicationUser ret = new ApplicationUser();
			
			var userStore = new UserStore<ApplicationUser>(db);
			var userManager = new UserManager<ApplicationUser>(userStore);

			List<ApplicationUser> list = userManager.Users.ToList();

			foreach (ApplicationUser a in list)
			{
				if(a.Id.Equals(id))
				{
					ret = a;
					break;
				}
			}

			if(ret != null)
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