using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

		IRepositoryDayType RepositoryDayTypes { get; set; }
		IRepositoryLine RepositoryLines { get; set; }
		IRepositoryPricelist RepositoryPricelists { get; set; }
		IRepositoryStation RepositoryStations { get; set; }
		IRepositoryTicketPrice RepositoryTicketPrices { get; set; }
		IRepositoryTicketType RepositoryTicketTypes { get; set; }
		IRepositoryTimetable RepositoryTimetables { get; set; }
		IRepositoryTimetableType RepositoryTimetableTypes { get; set; }
		IRepositoryUserType RepositoryUserTypes { get; set; }
		IRepositoryVehicle RepositoryVehicles { get; set; }

		int Complete();
    }
}
