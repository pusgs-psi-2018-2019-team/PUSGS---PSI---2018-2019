using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
	public class ProfilController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		public IUnitOfWork Db { get; set; }

		private const string LocalLoginProvider = "Local";
		private ApplicationUserManager _userManager;

		public ProfilController() { }

		public ProfilController(IUnitOfWork db)
		{
			this.Db = db;
		}

		// GET: api/Profil/User
		[AllowAnonymous]
		[ResponseType(typeof(ApplicationUser))]
		[Route("api/Profil/User")]
		public IHttpActionResult GetUser()
		{
			ApplicationUser ret = new ApplicationUser();

			var userStore = new UserStore<ApplicationUser>(db);
			var userManager = new UserManager<ApplicationUser>(userStore);

			string idUsera = User.Identity.GetUserId();
			if (idUsera != null)
			{
				var id = User.Identity.GetUserId();
				ret = userManager.FindById(id);				
			}
			else
			{
				return StatusCode(HttpStatusCode.BadRequest);
			}

			return Ok(ret);
		}

		[HttpPost]
		[AllowAnonymous]
		[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
		[Route("api/Profil/UpdateUser")]
		public IHttpActionResult EditUserInfo(RegisterBindingModel model)
		{

			var userStore = new UserStore<ApplicationUser>(db);
			var userManager = new UserManager<ApplicationUser>(userStore);
			ApplicationUser user = new ApplicationUser();
			string idUsera = User.Identity.GetUserId();
			if (idUsera != null)
			{
				var id = User.Identity.GetUserId();
				user = userManager.FindById(id);
			}

			user.Name = model.Name;
			user.Surname = model.Surname;
			user.Address = model.Address;
			user.Date = model.Date;
			user.Email = model.Email;
			user.PhoneNumber = model.PhoneNumber;
			//user.ImageUrl = model.ImageUrl;

			db.Entry(user).State = EntityState.Modified;

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