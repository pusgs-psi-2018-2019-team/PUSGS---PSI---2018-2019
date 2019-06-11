﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Models.BindingModels;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
	[Authorize]
	public class CenovnikController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		public IUnitOfWork Db { get; set; }

		private const string LocalLoginProvider = "Local";
		private ApplicationUserManager _userManager;

		public CenovnikController() { }

		public CenovnikController(IUnitOfWork db)
		{
			this.Db = db;
		}

		// GET: api/Cenovnik/UserType/{loggedUser}
		[AllowAnonymous]
		[ResponseType(typeof(string))]
		[Route("api/Cenovnik/UserType")]
		public IHttpActionResult GetUserType()
		{
			var userStore = new UserStore<ApplicationUser>(db);
			var userManager = new UserManager<ApplicationUser>(userStore);
			string type = "";
			try
			{
				var id = User.Identity.GetUserId();
				ApplicationUser user = userManager.FindById(id);

				var typeId = user.TypeId;

				type = Db.RepositoryUserTypes.Find(x => x.Id.Equals(typeId)).FirstOrDefault().Name;

			}
			catch (Exception e)
			{
				type = "neregistrovan";
			}

			return Ok(type);
		}

		// GET: api/Cenovnik/UserType/{loggedUser}
		[AllowAnonymous]
		[ResponseType(typeof(double))]
		[Route("api/Cenovnik/Cene/{tipKarte}/{tipKorisnika}")]
		public IHttpActionResult GetPrice(string tipKarte,string tipKorisnika)
		{
			int s = Db.RepositoryTicketTypes.Find(x => x.Name.Equals(tipKarte)).FirstOrDefault().Id;
			double ret = Db.RepositoryTicketPrices.Find(x => x.TicketTypeId.Equals(s)).FirstOrDefault().Price;

			if(tipKorisnika.Equals("Penzioner"))
			{
				ret = ret * 0.8;
			}
			else if (tipKorisnika.Equals("Đak"))
			{
				ret = ret * 0.9;
			}

			return Ok(ret);
		}

		// POST api/Cenovnik/KupiKartu
		[AllowAnonymous]
		[Route("api/Cenovnik/KupiKartu")]
		public IHttpActionResult KupiKartu(KupiKartuBindingModel karta)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (karta == null)
			{
				return BadRequest();
			}

			var userStore = new UserStore<ApplicationUser>(db);
			var userManager = new UserManager<ApplicationUser>(userStore);
			Ticket ticket = new Ticket();
			ApplicationUser user = new ApplicationUser();
			ticket.FinalPrice = karta.Price;
			string idUsera = User.Identity.GetUserId();
			DateTime vaziOd = new DateTime();
			DateTime vaziDo = new DateTime();

			if (idUsera != null)
			{
				var id = User.Identity.GetUserId();
				user = userManager.FindById(id);
				ticket.UserId = user.Id;
			}
			
			if (karta.TipKarte.Equals("Vremenska karta"))
			{
				vaziOd = DateTime.Now;
				vaziDo = DateTime.Now.AddHours(1);
			}
			else if (karta.TipKarte.Equals("Dnevna karta"))
			{
				vaziOd = DateTime.Now;
				vaziDo = KrajDana(DateTime.Now);
			}
			else if (karta.TipKarte.Equals("Mesečna karta"))
			{
				vaziOd = DateTime.Now;
				DateTime temp = DateTime.Now;
				temp = vaziOd.AddMonths(1);
				vaziDo = new DateTime(temp.Year, temp.Month, 1, 0, 0, 0);
			}
			else if (karta.TipKarte.Equals("Godišnja karta"))
			{
				vaziOd = DateTime.Now;
				DateTime temp = DateTime.Now;
				temp = vaziOd.AddYears(1);
				vaziDo = new DateTime(temp.Year, 1, 1, 0, 0, 0);
			}

			Pricelist pl = new Pricelist();
			pl.From = vaziOd.ToString();
			pl.To = vaziDo.ToString();

			try
			{
				db.Entry(pl).State = EntityState.Added;
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException e)
			{
				return StatusCode(HttpStatusCode.BadRequest);
			}

			Pricelist pricelist = db.Pricelist.Where(x => x.From.Equals(pl.From) && x.To.Equals(pl.To)).FirstOrDefault();
			ticket.PricelistId = pricelist.Id;

			try
			{
				db.Entry(ticket).State = EntityState.Added;
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException e)
			{
				return StatusCode(HttpStatusCode.BadRequest);
			}

			SendEmail("GSP Service", "kovacev.iceman@gmail.com", user.Email, "GSP Service, kupovina karte.", $"Salo pederu");
			return Ok("uspesno");
		}

		private void SendEmail(string sendername, string sender, string recipient, string subject, string body)
		{
			// SMTP server,port,username,password should be obtained from C:\cornerstone\CFMLocal.txt (line 2?)
			SmtpClient smtpClient = new SmtpClient("smtp.mailtrap.io", 2525)
			{
				// Milan's mail trap free SMTP credentials : u: "3af75f9040edca", p: "bc2ed058a47d71"  | host: "smtp.mailtrap.io", 2525
				Credentials = new System.Net.NetworkCredential()
				{
					UserName = "3af75f9040edca",
					Password = "bc2ed058a47d71"
				},

				EnableSsl = true
			};

			MailAddress from = new MailAddress(sender, sendername);
			MailAddress to = new MailAddress(recipient, "");
			MailMessage mailMessage = new MailMessage(from, to)
			{
				Subject = subject,
				Body = body
			};



			smtpClient.Send(mailMessage);
		}

		private bool KartaExists(int id)
		{
			return db.Ticket.Count(e => e.Id == id) > 0;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool TimetableExist(int id)
		{
			return Db.RepositoryTimetables.GetAll().Count(e => e.Id == id) > 0;
		}

		public static DateTime PocetakDana(DateTime dateTime)
		{
			return dateTime.Date;
		}
		
		public static DateTime KrajDana(DateTime dateTime)
		{
			return PocetakDana(dateTime).AddDays(1).AddTicks(-1);
		}

	}
}