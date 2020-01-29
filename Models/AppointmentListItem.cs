using Fox.Microservices.Diary.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fox.Microservices.Diary.Models
{
	public class AppointmentListItem : AppointmentBase
	{
		public string StatusDescription { get; set; }
		public string EmployeeName { get; set; }
		public string CustomerName { get; set; }
		public string ServiceDescription { get; set; }
		public string AppointmentShopDescription { get; set; }
		public string AreaCode { get; set; }
		public string AreaDescription { get; set; }
		public string RegionCode { get; set; }
		public string RegionDescription { get; set; }
		public string MediaTypeDescription { get; set; }
		public string ShopDescription { get; set; }
		public string RoomDescription { get; set; }

		public AppointmentListItem()
		{

		}

		public AppointmentListItem(AG_B_APPOINTMENT Entity) : base(Entity)
		{
		}



		public override void LoadData<T>(DbContext context, dynamic entity)
		{
			base.LoadData<T>(context, (T)entity);

			DiaryContext DBContext = (DiaryContext)context;
			AG_B_APPOINTMENT appointment = (AG_B_APPOINTMENT)entity;

			//EmployeeName = string.Format("{0} {1}", appointment.CM_S_EMPLOYEE?.FIRSTNAME, appointment.CM_S_EMPLOYEE?.LASTNAME);
			EmployeeName = appointment.CM_S_EMPLOYEE?.EMPLOYEE_DESCR;
			ServiceDescription = appointment.AG_S_SERVICE?.SERVICE_DESCR;
			
			StatusDescription = DBContext.SY_GENERAL_STATUS.FirstOrDefault(E => E.STATUS_CODE == StatusCode)?.STATUS_DESCR;
			CU_B_ADDRESS_BOOK Customer = DBContext.CU_B_ADDRESS_BOOK.FirstOrDefault(E => E.CUSTOMER_CODE == CustomerCode);
			if (Customer != null)
				CustomerName = string.Format("{0} {1}", Customer.FIRSTNAME, Customer.LASTNAME);
			CM_B_SHOP Shop = DBContext.CM_B_SHOP.FirstOrDefault(E => E.SHOP_CODE == AppointmentShopCode);
			AppointmentShopDescription = Shop?.SHOP_DESCR;
			CM_S_CITY_BOOK_SHOP ShopArea = DBContext.CM_S_CITY_BOOK_SHOP.FirstOrDefault(E => E.SHOP_CODE == AppointmentShopCode);
			AreaCode = ShopArea?.AREA_CODE;
			CM_S_AREA_BOOK AreaBook = DBContext.CM_S_AREA_BOOK.FirstOrDefault(E => E.AREA_CODE == AreaCode);
			AreaDescription = AreaBook?.AREA_DESCR;
			RegionCode = AreaBook?.REGION_CODE;
			CM_S_REGION_BOOK Region = DBContext.CM_S_REGION_BOOK.FirstOrDefault(E => E.REGION_CODE == RegionCode);
			RegionDescription = Region?.REGION_DESCR;
			CU_B_ACTIVITY activity = DBContext.CU_B_ACTIVITY.FirstOrDefault(E => E.ACTIVITY_TYPE_CODE == "PR" && E.CUSTOMER_CODE == appointment.CUSTOMER_CODE && E.APPOINTMENT_ID == appointment.APPOINTMENT_ID);
			MediaTypeDescription = activity?.CM_S_MEDIATYPE?.MEDIATYPE_DESCR;
			AG_S_ROOM room = DBContext.AG_S_ROOM.FirstOrDefault(E => E.SHOP_CODE == appointment.APPOINTMENT_SHOP_CODE && E.ROOM_CODE == appointment.ROOM_CODE);
			RoomDescription = room?.ROOM_DESCR;
		}
	}
}
