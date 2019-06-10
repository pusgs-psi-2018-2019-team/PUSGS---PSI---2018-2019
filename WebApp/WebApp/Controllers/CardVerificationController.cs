using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize]
    public class CardVerificationController : ApiController
    {
        private IUnitOfWork db;

        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public CardVerificationController() { }

        public CardVerificationController(IUnitOfWork db)
        {
            this.db = db;
        }

        // GET: api/CardVerification/Check/{id}
        [Route("api/CardVerification/Check/{id}")]
        [HttpGet]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetCard(int id) 
        {
            Ticket ticket = db.RepositoryTicket.Find(x => x.Id.Equals(id)).FirstOrDefault();

            if (ticket != null)
                return Ok("true");
            else
                return Ok("false");
        }
    }
}
