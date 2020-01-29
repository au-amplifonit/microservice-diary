using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITools.Models.Configuration;

namespace Fox.Microservices.Diary.Models.Configuration
{
	public class CustomAppSettings: AppSettings
	{
		public int ScreeningAppointmentMonthTolerance { get; set; } = 18;
	}
}
