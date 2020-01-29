using Fox.Microservices.Diary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITools.EntityMapper;
using WebAPITools.Models;

namespace Fox.Microservices.Diary.Models
{
	public class AppointmentBase : ModelBase
	{
		[FieldMapper(nameof(AG_B_APPOINTMENT.APPOINTMENT_ID))]
		public string AppointmentID { get; set; }

		[FieldMapper(nameof(AG_B_APPOINTMENT.STATUS_CODE), DefaultValue = "A")]
		public string StatusCode { get; set; }

		[FieldMapper(nameof(AG_B_APPOINTMENT.DT_APPOINTMENT))]
		public DateTime AppointmentDate { get; set; }

		[FieldMapper(nameof(AG_B_APPOINTMENT.DURATION))]
		public int Duration { get; set; }

		[FieldMapper(nameof(AG_B_APPOINTMENT.EMPLOYEE_CODE))]
		public string EmployeeCode { get; set; }

		[FieldMapper(nameof(AG_B_APPOINTMENT.CUSTOMER_CODE))]
		public string CustomerCode { get; set; }

		[FieldMapper(nameof(AG_B_APPOINTMENT.SHOP_CODE))]
		public string ShopCode { get; set; }

		[FieldMapper(nameof(AG_B_APPOINTMENT.APPOINTMENT_SHOP_CODE))]
		public string AppointmentShopCode { get; set; }

		[FieldMapper(nameof(AG_B_APPOINTMENT.SERVICE_CODE))]
		public string ServiceCode { get; set; }

		[FieldMapper(nameof(AG_B_APPOINTMENT.ROOM_CODE))]
		public string RoomCode { get; set; }

		[FieldMapper(nameof(AG_B_APPOINTMENT.NOTE))]
		public string Note { get; set; }

		public AppointmentBase()
		{
		}

		public AppointmentBase(AG_B_APPOINTMENT Entity) : base(Entity.ROWGUID)
		{
			AppointmentID = Entity.APPOINTMENT_ID;
			StatusCode = Entity.STATUS_CODE;
			AppointmentDate = Entity.DT_APPOINTMENT;
			Duration = Entity.DURATION.GetValueOrDefault();
			EmployeeCode = Entity.EMPLOYEE_CODE;
			CustomerCode = Entity.CUSTOMER_CODE;
			AppointmentShopCode = Entity.APPOINTMENT_SHOP_CODE;
			ServiceCode = Entity.SERVICE_CODE;
			Note = Entity.NOTE;
		}
	}
}
