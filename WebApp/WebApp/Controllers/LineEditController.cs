using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    public class LineEditController : ApiController
    {
        private IUnitOfWork db;

        public LineEditController() { }

        public LineEditController(IUnitOfWork db)
        {
            this.db = db;
        }

        [Route("api/LineEdit/getAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var lines = db.RepositoryLines.GetAll();

            return Ok(lines);
        }
    }
}
