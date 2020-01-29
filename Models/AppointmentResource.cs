using Fox.Microservices.Diary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITools.EntityMapper;
using WebAPITools.Models;

namespace Fox.Microservices.Diary.Models
{
	public class AppointmentResource: ModelBase
	{
		[FieldMapper(nameof(CM_S_EMPLOYEE.EMPLOYEE_CODE))]
		public string Code { get; set; }

		[FieldMapper(nameof(CM_S_EMPLOYEE.EMPLOYEE_DESCR))]
		public string Description { get; set; }

		[FieldMapper(nameof(CM_S_EMPLOYEE.EMPLOYEE_TYPE_CODE))]
		public string ResourceTypeCode { get; set; }
	}
}
