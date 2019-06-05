using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.BindingModels
{
	public class RedVoznjeInfoBindingModel
	{
		public List<TimetableType> TimetableTypes { get; set; }
		public List<Line> Lines { get; set; }
		public List<DayType> DayTypes { get; set; }
	}
}