using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fox.Microservices.CommonUtils;
using Fox.Microservices.Diary.Helpers;
using Fox.Microservices.Diary.Models;
using Fox.Microservices.Diary.Models.Entities;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPITools.EntityMapper;
using WebAPITools.Models.Configuration;
using LinqKit;
using System.Linq.Expressions;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Fox.Microservices.Common.Data.Models;
using System.Text;
using WebAPITools.ErrorHandling;
using Fox.Microservices.Common.Data.Helpers;
using WebAPITools.Models;
using Fox.Microservices.Diary.Models.Configuration;

namespace Fox.Microservices.Diary.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class DiaryController : ControllerBase
	{
		private readonly IOptions<CustomAppSettings> Settings;
		private readonly DiaryContext DBContext;
		private readonly IFoxDataService FoxDataService;
		private string[] AllowedAppointmentStatus = { "A", "D", "U", "W" };
		private readonly AppointmentStatusHelper appointmentStatus;
		private readonly IHttpContextAccessor HttpContextAccessor;


		public DiaryController(IOptions<CustomAppSettings> ASettings, DiaryContext ADBContext, IFoxDataService foxDataService, IHttpContextAccessor httpContextAccessor)
		{
			Settings = ASettings;
			HttpContextAccessor = httpContextAccessor;
			if (Settings != null)
				Settings.Value.Configure(httpContextAccessor);
			EntityMapper.Settings = Settings.Value;
			DBContext = ADBContext;
			FoxDataService = foxDataService;
			appointmentStatus = new AppointmentStatusHelper(FoxDataService);
		}

		/// <summary>
		/// Given a customer ID returns all customer's appointments
		/// </summary>
		/// <param name="id">The customer ID</param>
		/// <param name="MinDate">Minimum date from which show appointments (Format YYYY-MM-DD).
		/// If not set use today as default value.
		/// To get all the customer's appointments use 0001-01-01</param>
		/// <returns>Customer's addresses</returns>
		[HttpGet("[action]/{id}")]
		public ActionResult<IEnumerable<AppointmentListItem>> Customer(string id, DateTime? MinDate, int? MaxResultsCount)
		{
			DateTime MinSQLDateTime = new DateTime(1753, 1, 1);
			List<AppointmentListItem> Result = new List<AppointmentListItem>();
			DateTime MinAppDate = MinDate.GetValueOrDefault(MinSQLDateTime);
			MaxResultsCount = MaxResultsCount.GetValueOrDefault(Settings.Value.MaxQueryResult);
			if (MinAppDate <= MinSQLDateTime)
				MinAppDate = MinSQLDateTime;
			foreach (AG_B_APPOINTMENT Item in DBContext.AG_B_APPOINTMENT.Where(E => E.CUSTOMER_CODE == id && E.DT_APPOINTMENT >= MinAppDate.Date).OrderByDescending(E => E.DT_APPOINTMENT).Take(MaxResultsCount.Value))
			{
				AppointmentListItem ListItem = EntityMapper.Map<AppointmentListItem, AG_B_APPOINTMENT>(DBContext, Item, Item.AG_B_APPOINTMENT_EXT_AUS);
				Result.Add(ListItem);
			}
			return Result;
		}

		/// <summary>
		/// Returns all the appointment booked for the shopt on a specific date
		/// </summary>
		/// <param name="id">Shop code</param>
		/// <param name="serviceCodes">appointment service code filter (| separeted)</param>
		/// <param name="statusCodes">appointment status code filter (| separeted)</param>
		/// <param name="employeeCodes">appointment employee code filter (| separeted)</param>
		/// <param name="appointmentDate">Appointment date (default today)</param>
		/// <param name="MaxResultsCount">Max record to be returned</param>
		/// <returns></returns>
		[HttpGet("[action]/{id}")]
		public ActionResult<IEnumerable<AppointmentListItem>> Shop(string id, string serviceCodes, string statusCodes, string employeeCodes, DateTime? appointmentDate, int? MaxResultsCount)
		{
			DateTime MinSQLDateTime = new DateTime(1753, 1, 1);
			List<AppointmentListItem> Result = new List<AppointmentListItem>();
			DateTime AppDate = appointmentDate.GetValueOrDefault(DateTime.Today);
			MaxResultsCount = MaxResultsCount.GetValueOrDefault(Settings.Value.MaxQueryResult);
			if (AppDate <= MinSQLDateTime)
				AppDate = MinSQLDateTime;
			string[] unavailabilityServiceCodes = FoxDataService.GetUnavailabilityServiceCodes(id);

			var predicate = PredicateBuilder.New<AG_B_APPOINTMENT>();
			predicate = predicate.And(E => E.APPOINTMENT_SHOP_CODE == id && E.DT_APPOINTMENT.Date == AppDate.Date && !unavailabilityServiceCodes.Contains(E.SERVICE_CODE));

			if (!string.IsNullOrWhiteSpace(serviceCodes))
			{
				string[] codes = serviceCodes.Split("|");
				predicate = predicate.And(E => codes.Contains(E.SERVICE_CODE));
			}

			if (!string.IsNullOrWhiteSpace(statusCodes))
			{
				string[] codes = statusCodes.Split("|");
				predicate = predicate.And(E => codes.Contains(E.STATUS_CODE));
			}

			if (!string.IsNullOrWhiteSpace(employeeCodes))
			{
				string[] codes = employeeCodes.Split("|");
				predicate = predicate.And(E => codes.Contains(E.EMPLOYEE_CODE));
			}

			foreach (AG_B_APPOINTMENT Item in DBContext.AG_B_APPOINTMENT.Where(predicate).OrderByDescending(E => E.DT_APPOINTMENT).Take(MaxResultsCount.Value))
			{
				AppointmentListItem ListItem = EntityMapper.Map<AppointmentListItem, AG_B_APPOINTMENT>(DBContext, Item, Item.AG_B_APPOINTMENT_EXT_AUS);
				Result.Add(ListItem);
			}
			return Result;
		}

		/// <summary>
		/// Retrieve appointment data
		/// </summary>
		/// <param name="rowGuid">Appointment rowGuid</param>
		/// <returns></returns>
		[HttpGet("{rowGuid}")]
		public ActionResult<AppointmentListItem> Get(Guid rowGuid)
		{
			AppointmentListItem Result = null;
			AG_B_APPOINTMENT Item = DBContext.AG_B_APPOINTMENT.FirstOrDefault(E => E.ROWGUID == rowGuid);

			if (Item == null)
				throw new NotFoundException($"No appointment found with rowGuid: {rowGuid}");

			Result = EntityMapper.Map<AppointmentListItem, AG_B_APPOINTMENT>(DBContext, Item, Item.AG_B_APPOINTMENT_EXT_AUS);

			return Result;
		}


		/// <summary>
		/// Insert an appointment
		/// </summary>
		/// <param name="value">Appointment data</param>
		/// <param name="campaignCode">Campaign code associated with the new appointment (optional)</param>
		/// <param name="mediaTypeCode">Campaign media type code associated with the new appointment (optional)</param>
		/// <param name="callID">CallID associated with the new appointment (optional)</param>
		/// <returns></returns>
		// POST api/values
		[HttpPost]
		public ActionResult<AppointmentListItem> Post([FromBody] AppointmentBase value, string campaignCode = null, string mediaTypeCode = null, string callID = null)
		{
			return DoPost(value, campaignCode, mediaTypeCode, callID);
		}

		private AppointmentListItem DoPost(AppointmentBase value, string campaignCode = null, string mediaTypeCode = null, string callID = null, Guid? rescheduledAppointmentRowGuid = null)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value), "Argument 'value' cannot be null");

			OperationResult<bool> check = CheckCanBookAppointment(value.CustomerCode, value.AppointmentShopCode, value.ServiceCode, value.AppointmentDate, rescheduledAppointmentRowGuid.GetValueOrDefault(Guid.Empty));
			if (!check.Result)
				throw new InvalidOperationException(check.Message);

			AG_B_APPOINTMENT Item = EntityMapper.CreateEntity<AG_B_APPOINTMENT>();
			AG_B_APPOINTMENT_EXT_AUS ItemExt = EntityMapper.CreateEntity<AG_B_APPOINTMENT_EXT_AUS>();

			CU_B_ACTIVITY activity = EntityMapper.CreateEntity<CU_B_ACTIVITY>();
			CU_B_ACTIVITY_EXT_AUS activityExt = EntityMapper.CreateEntity<CU_B_ACTIVITY_EXT_AUS>();

			AG_B_APPOINTMENT_STATUS_HISTORY history = EntityMapper.CreateEntity<AG_B_APPOINTMENT_STATUS_HISTORY>();

			if ((string.IsNullOrWhiteSpace(value.StatusCode) || value.StatusCode == appointmentStatus.Open) && value.AppointmentDate >= DateTime.Today && value.AppointmentDate < DateTime.Today.AddDays(2))
				value.StatusCode = appointmentStatus.Confirmed;

			EntityMapper.UpdateEntity(value, Item, ItemExt);

			//Add default or other values
			Item.APPOINTMENT_ID = ItemExt.APPOINTMENT_ID = string.Format("{0}00{1}", Item.APPOINTMENT_SHOP_CODE, FoxDataService.GetNewCounter("AG_B_APPOINTMENT", "APPOINTMENT_ID", "*", Item.USERINSERT).FORMATTEDVALUE);
			if (string.IsNullOrEmpty(Item.SHOP_CODE))
				Item.SHOP_CODE = ItemExt.SHOP_CODE = Item.LAPTOP_CODE = ItemExt.LAPTOP_CODE = Item.APPOINTMENT_SHOP_CODE;

			string[] specialAvailabilityServiceCodes = FoxDataService.GetSpecialAvailabilityServiceCodes(Item.SHOP_CODE);

			CM_B_SHOP shop = DBContext.CM_B_SHOP.FirstOrDefault(E => E.SHOP_CODE == Item.SHOP_CODE);

			activity.ACTIVITY_TYPE_CODE = "PR";
			activity.ACTIVITY_DATE = DateTime.Today;
			activity.CUSTOMER_CODE = activityExt.CUSTOMER_CODE = Item.CUSTOMER_CODE;
			activity.EMPLOYEE_CODE = Item.EMPLOYEE_CODE;
			Item.LOCATION_CODE = activity.LOCATION_CODE = shop?.OBJ_CODE;
			Item.LOCATION_TYPE_CODE = activity.LOCATION_TYPE_CODE = Item.SHOP_CODE == "000" ? "03" : "01";
			Item.STATUS_CODE = Item.STATUS_CODE ?? appointmentStatus.Open;
			activity.REFERENCE_DATE = Item.DT_APPOINTMENT;
			activity.REFERENCE_NUMBER = Item.APPOINTMENT_ID;
			history.APPOINTMENT_ID = activity.APPOINTMENT_ID = Item.APPOINTMENT_ID;
			history.SHOP_CODE = history.LAPTOP_CODE = activity.SHOP_CODE = activity.LAPTOP_CODE = activityExt.SHOP_CODE = activityExt.LAPTOP_CODE = Item.SHOP_CODE;
			activity.ACTIVITY_ID = activityExt.ACTIVITY_ID = FoxDataService.GetNewCounter("CU_B_ACTIVITY", "ACTIVITY_ID", Item.SHOP_CODE, Item.USERINSERT).VALUE.GetValueOrDefault();
			history.CONFIRMATION_STATUS = Item.STATUS_CODE;
			history.REASON_CODE = Item.REASON_CODE;
			history.ROWGUID = Item.ROWGUID;


			/* Disabled FK check 
			//Check for valid campaign
			if (!string.IsNullOrWhiteSpace(campaignCode))
			{
				CM_S_CAMPAIGN campaign = DBContext.CM_S_CAMPAIGN.FirstOrDefault(E => E.CAMPAIGN_CODE == campaignCode);
				ItemExt.CAMPAIGN_CODE = activity.CAMPAIGN_CODE = campaign?.CAMPAIGN_CODE;
			}
			//Check for valid mediatype
			if (!string.IsNullOrWhiteSpace(mediaTypeCode))
			{
				CM_S_MEDIATYPE mediaType = DBContext.CM_S_MEDIATYPE.FirstOrDefault(E => E.MEDIATYPE_CODE == mediaTypeCode);
				ItemExt.MEDIATYPE_CODE = activity.MEDIATYPE_CODE = mediaType?.MEDIATYPE_CODE;
			}
			*/

			ItemExt.CAMPAIGN_CODE = activity.CAMPAIGN_CODE = campaignCode;
			ItemExt.MEDIATYPE_CODE = activity.MEDIATYPE_CODE = mediaTypeCode;

			ItemExt.SOURCE_TRACKING_ID = callID;

			EntityMapper.CheckEntityRowId(activity, activityExt, Guid.NewGuid());

			value.SaveData<AG_B_APPOINTMENT>(DBContext, Item);
			value.SaveData<CU_B_ACTIVITY>(DBContext, activity);

			// Set proper customer shop code if needed
			CU_B_ADDRESS_BOOK customer = DBContext.CU_B_ADDRESS_BOOK.FirstOrDefault(E => E.CUSTOMER_CODE == Item.CUSTOMER_CODE && E.CU_B_ADDRESS_BOOK_EXT_AUS.SHOP_CODE == "000");
			if (customer != null)
				customer.CU_B_ADDRESS_BOOK_EXT_AUS.SHOP_CODE = Item.APPOINTMENT_SHOP_CODE;


			DBContext.AG_B_APPOINTMENT.Add(Item);
			DBContext.AG_B_APPOINTMENT_EXT_AUS.Add(ItemExt);
			DBContext.CU_B_ACTIVITY.Add(activity);
			DBContext.CU_B_ACTIVITY_EXT_AUS.Add(activityExt);
			DBContext.AG_B_APPOINTMENT_STATUS_HISTORY.Add(history);


			IDbContextTransaction scope = DBContext.Database.CurrentTransaction;
			bool ownTransaction = (scope == null);
			if (scope == null)
				scope = DBContext.Database.BeginTransaction();

			try
			{
				//Lock the table during this transaction
				DBContext.AG_B_APPOINTMENT.FromSql("SELECT TOP 1 * FROM AG_B_APPOINTMENT WITH (TABLOCKX, HOLDLOCK)");

				//Check for overlap
				DateTime appointmentStart = Item.DT_APPOINTMENT;
				DateTime appointmentEnd = Item.DT_APPOINTMENT.AddMinutes(Item.DURATION.GetValueOrDefault());

				int overlappedAppointmentCount = DBContext.AG_B_APPOINTMENT.Where(E => AllowedAppointmentStatus.Contains(E.STATUS_CODE)
																			&& (E.ROOM_CODE == Item.ROOM_CODE || E.EMPLOYEE_CODE == Item.EMPLOYEE_CODE)
																			&& !specialAvailabilityServiceCodes.Contains(E.AG_S_SERVICE.SERVICE_TYPE_CODE) &&
																			((E.DT_APPOINTMENT >= appointmentStart && E.DT_APPOINTMENT < appointmentEnd)
																			|| (E.DT_APPOINTMENT.AddMinutes(E.DURATION.GetValueOrDefault()) > appointmentStart && E.DT_APPOINTMENT.AddMinutes(E.DURATION.GetValueOrDefault()) < appointmentEnd)
																			|| (E.DT_APPOINTMENT <= appointmentStart && E.DT_APPOINTMENT.AddMinutes(E.DURATION.GetValueOrDefault()) > appointmentEnd))).Count();
				if (overlappedAppointmentCount > 0)
					throw new Exception("There is already an appointment for the given time slot");

				//Complete the scope here to commit, otherwise it will rollback
				//The table lock will be released after we exit the TransactionScope block
				DBContext.SaveChanges();
				if (ownTransaction)
					scope.Commit();
			}
			catch(Exception E)
			{
				if (ownTransaction)
					scope.Rollback();
				throw E;
			}

			return Get(Item.ROWGUID).Value;
		}

		/// <summary>
		/// Update an appointment
		/// </summary>
		/// <param name="rowGuid">Appointment rowGuid</param>
		/// <param name="value">Appointment data</param>
		// PUT api/values
		[HttpPut("{rowGuid}")]
		public void Put(Guid rowGuid, [FromBody] AppointmentBase value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value), $"Argument value cannot be null");

			AG_B_APPOINTMENT Item = DBContext.AG_B_APPOINTMENT.SingleOrDefault(E => E.ROWGUID == rowGuid);
			if (Item == null)
				throw new NotFoundException($"No appointment found with RowGuid:{rowGuid}");

			OperationResult<bool> check = CheckCanBookAppointment(value.CustomerCode, value.AppointmentShopCode, value.ServiceCode, value.AppointmentDate, rowGuid);
			if (!check.Result)
				throw new InvalidOperationException(check.Message);

			EntityMapper.UpdateEntity(value, Item, Item.AG_B_APPOINTMENT_EXT_AUS);

			value.SaveData<AG_B_APPOINTMENT>(DBContext, Item);

			if (string.IsNullOrEmpty(Item.SHOP_CODE))
				Item.SHOP_CODE = Item.AG_B_APPOINTMENT_EXT_AUS.SHOP_CODE = Item.LAPTOP_CODE = Item.AG_B_APPOINTMENT_EXT_AUS.LAPTOP_CODE = Item.APPOINTMENT_SHOP_CODE;


			CM_B_SHOP shop = DBContext.CM_B_SHOP.FirstOrDefault(E => E.SHOP_CODE == Item.SHOP_CODE);


			value.SaveData<AG_B_APPOINTMENT>(DBContext, Item);

			using (IDbContextTransaction scope = DBContext.Database.BeginTransaction())
			{
				CheckAppointmentOverlap(Item);
				//Complete the scope here to commit, otherwise it will rollback
				//The table lock will be released after we exit the TransactionScope block
				DBContext.SaveChanges();
				scope.Commit();
			}
		}

		void CheckAppointmentOverlap(AG_B_APPOINTMENT appointment)
		{
			string[] specialAvailabilityServiceCodes = FoxDataService.GetSpecialAvailabilityServiceCodes(appointment.SHOP_CODE);
			//Lock the table during this transaction
			DBContext.AG_B_APPOINTMENT.FromSql("SELECT TOP 1 * FROM AG_B_APPOINTMENT WITH (TABLOCKX, HOLDLOCK)");

			//Check for overlap
			DateTime appointmentStart = appointment.DT_APPOINTMENT;
			DateTime appointmentEnd = appointment.DT_APPOINTMENT.AddMinutes(appointment.DURATION.GetValueOrDefault());

			int overlappedAppointmentCount = DBContext.AG_B_APPOINTMENT.Where(E => E.ROWGUID != appointment.ROWGUID && AllowedAppointmentStatus.Contains(E.STATUS_CODE)
																		&& (E.ROOM_CODE == appointment.ROOM_CODE || E.EMPLOYEE_CODE == appointment.EMPLOYEE_CODE)
																		&& !specialAvailabilityServiceCodes.Contains(E.AG_S_SERVICE.SERVICE_TYPE_CODE) &&
																		((E.DT_APPOINTMENT >= appointmentStart && E.DT_APPOINTMENT < appointmentEnd)
																		|| (E.DT_APPOINTMENT.AddMinutes(E.DURATION.GetValueOrDefault()) > appointmentStart && E.DT_APPOINTMENT.AddMinutes(E.DURATION.GetValueOrDefault()) < appointmentEnd)
																		|| (E.DT_APPOINTMENT <= appointmentStart && E.DT_APPOINTMENT.AddMinutes(E.DURATION.GetValueOrDefault()) > appointmentEnd))).Count();
			if (overlappedAppointmentCount > 0)
				throw new Exception("There is already an appointment for the given time slot");
		}

		[HttpPut("[action]/{rescheduledAppointmentRowGuid}")]
		public ActionResult<AppointmentListItem> Reschedule(Guid rescheduledAppointmentRowGuid, [FromBody] AppointmentBase value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value), $"Argument value cannot be null");

			AG_B_APPOINTMENT oldAppointment = DBContext.AG_B_APPOINTMENT.SingleOrDefault(E => E.ROWGUID == rescheduledAppointmentRowGuid);
			if (oldAppointment == null)
				throw new NotFoundException($"No appointment found with RowGuid:{rescheduledAppointmentRowGuid}");

			if (oldAppointment.AG_B_APPOINTMENT_EXT_AUS.RESCHEDULED_NUMBER.GetValueOrDefault(0) >= 3)
				throw new InvalidOperationException("Appointment reschedule max number exceeded");

			oldAppointment.AG_B_APPOINTMENT_EXT_AUS.RESCHEDULED_NUMBER = oldAppointment.AG_B_APPOINTMENT_EXT_AUS.RESCHEDULED_NUMBER.GetValueOrDefault(0) + 1;

			using (IDbContextTransaction scope = DBContext.Database.BeginTransaction())
			{
				UpdateAppointmentStatus(oldAppointment.ROWGUID, appointmentStatus.Rescheduled);
				AppointmentListItem Result = DoPost(value, rescheduledAppointmentRowGuid: rescheduledAppointmentRowGuid);

				CU_B_ACTIVITY activity = EntityMapper.CreateEntity<CU_B_ACTIVITY>();
				CU_B_ACTIVITY_EXT_AUS activityExt = EntityMapper.CreateEntity<CU_B_ACTIVITY_EXT_AUS>();
				activity.ACTIVITY_TYPE_CODE = "DY";
				activity.ACTIVITY_DATE = DateTime.Today;
				activity.CUSTOMER_CODE = activityExt.CUSTOMER_CODE = Result.CustomerCode;
				activity.EMPLOYEE_CODE = Result.EmployeeCode;
				activity.REFERENCE_DATE = oldAppointment.DT_APPOINTMENT;
				activity.REFERENCE_NUMBER = oldAppointment.APPOINTMENT_ID;
				activity.APPOINTMENT_ID = Result.AppointmentID;
				activityExt.DT_APPOINTEMENT_FROM = oldAppointment.DT_APPOINTMENT;
				activityExt.DT_APPOINTEMENT_TO = Result.AppointmentDate;
				activity.ACTIVITY_ID = activityExt.ACTIVITY_ID = FoxDataService.GetNewCounter("CU_B_ACTIVITY", "ACTIVITY_ID", Result.StatusCode, activity.USERINSERT).VALUE.GetValueOrDefault();
				activity.SHOP_CODE = activity.LAPTOP_CODE = activityExt.SHOP_CODE = activityExt.LAPTOP_CODE = Result.ShopCode;
				DBContext.CU_B_ACTIVITY.Add(activity);
				DBContext.CU_B_ACTIVITY_EXT_AUS.Add(activityExt);

				DBContext.SaveChanges();
				scope.Commit();

				return Result;
			}			
		}

		/// <summary>
		/// Return the next (or the last if there's no next one) appointment for a given customer
		/// </summary>
		/// <param name="id">Customer's ID</param>
		/// <returns>Next (or Last) customer's appointment info</returns>
		[HttpGet("[action]/{id}")]
		public ActionResult<AppointmentListItem> CustomerNextAppointment(string id)
		{
			AppointmentListItem Result = null;
			//Take next appointment
			AG_B_APPOINTMENT Appointment = DBContext.AG_B_APPOINTMENT.OrderBy(E => E.DT_APPOINTMENT).FirstOrDefault(E => E.CUSTOMER_CODE == id && E.DT_APPOINTMENT >= DateTime.Today && AllowedAppointmentStatus.Contains(E.STATUS_CODE));
			if (Appointment == null) //Take last appointment			
				Appointment = DBContext.AG_B_APPOINTMENT.OrderByDescending(E => E.DT_APPOINTMENT).FirstOrDefault(E => E.CUSTOMER_CODE == id && E.DT_APPOINTMENT < DateTime.Today);
			
			if (Appointment != null)
				Result = EntityMapper.Map<AppointmentListItem, AG_B_APPOINTMENT>(DBContext, Appointment, Appointment.AG_B_APPOINTMENT_EXT_AUS);

			return Result;
		}

		/// <summary>
		/// Given a customer ID returns all customer's appointments eventually filtered by date and/or Service Codes and/or Status codes
		/// </summary>
		/// <param name="id">The customer ID</param>
		/// <param name="MinDate">Minimum date from which show appointments (Format YYYY-MM-DD).
		/// If not set use today as default value.
		/// To get all the customer's appointments use 0001-01-01</param>
		/// <param name="ServiceCodes">Service codes to filter, separated by | (pipe)</param>
		/// <param name="StatusCodes">Status codes to filter, separated by | (pipe)</param>
		/// <returns>Customer's appointments</returns>
		[HttpGet("[action]/{id}")]
		public ActionResult<IEnumerable<AppointmentListItem>> CustomerFiltered(string id, DateTime? MinDate, string ServiceCodes, string StatusCodes)
		{
			DateTime MinSQLDateTime = new DateTime(1753, 1, 1);
			string[] ServiceCodeList = null;
			string[] StatusCodeList = null;
			List<AppointmentListItem> Result = new List<AppointmentListItem>();
			DateTime MinAppDate = MinDate.GetValueOrDefault(DateTime.Today);
			if (MinAppDate <= MinSQLDateTime)
				MinAppDate = MinSQLDateTime;
			if (!string.IsNullOrWhiteSpace(ServiceCodes))
				ServiceCodeList = ServiceCodes.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
			if (!string.IsNullOrWhiteSpace(StatusCodes))
				StatusCodeList = StatusCodes.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (AG_B_APPOINTMENT Item in DBContext.AG_B_APPOINTMENT.Where(E => E.CUSTOMER_CODE == id && E.DT_APPOINTMENT >= MinAppDate.Date
					&& (ServiceCodeList == null || ServiceCodeList.Contains(E.SERVICE_CODE))
					&& (StatusCodeList == null || StatusCodeList.Contains(E.STATUS_CODE))).OrderByDescending(E => E.DT_APPOINTMENT).Take(Settings.Value.MaxQueryResult))
			{
				AppointmentListItem ListItem = EntityMapper.Map<AppointmentListItem, AG_B_APPOINTMENT>(DBContext, Item, Item.AG_B_APPOINTMENT_EXT_AUS);
				Result.Add(ListItem);
			}
			return Result;
		}

		[HttpGet("[action]")]
		public ActionResult<IEnumerable<AppointmentResource>> GetAvailableAppointmentResources(string shopCode, string serviceCode, DateTime slotStart, short slotSize)
		{
			string[] ScreeningEmployeeTypeCodes = { "SRO" };
			string[] AppointmentEmployeeTypeCodes = { "AUD" };
			string[] EmployeeTypeCodes = ScreeningEmployeeTypeCodes;

			if (shopCode == null)
				throw new ArgumentNullException(nameof(shopCode), "shopCode cannot be null");
			if (serviceCode == null)
				throw new ArgumentNullException(nameof(serviceCode), "ServiceCode cannot be null");

			DateTime slotEnd = slotStart.AddMinutes(slotSize);
			string[] screeningServiceCodes = FoxDataService.GetScreeningServiceCodes(shopCode);
			string[] specialAvailabilityServiceCodes = FoxDataService.GetSpecialAvailabilityServiceCodes(shopCode);

			List <AppointmentResource> Result = new List<AppointmentResource>();

			if (!screeningServiceCodes.Contains(serviceCode.ToUpper())) //Screening
				EmployeeTypeCodes = AppointmentEmployeeTypeCodes;

			string[] bookedResources = DBContext.AG_B_APPOINTMENT.Where(E => E.SHOP_CODE == shopCode 
				&& AllowedAppointmentStatus.Contains(E.STATUS_CODE) && !specialAvailabilityServiceCodes.Contains(E.AG_S_SERVICE.SERVICE_TYPE_CODE) &&
					((E.DT_APPOINTMENT >= slotStart && E.DT_APPOINTMENT <= slotEnd)
						|| (E.DT_APPOINTMENT.AddMinutes(E.DURATION.GetValueOrDefault()) > slotStart && E.DT_APPOINTMENT.AddMinutes(E.DURATION.GetValueOrDefault()) <= slotEnd)
						|| (E.DT_APPOINTMENT <= slotStart && E.DT_APPOINTMENT.AddMinutes(E.DURATION.GetValueOrDefault()) >= slotEnd))).Select(E => E.EMPLOYEE_CODE).Distinct().ToArray();

			IEnumerable<CM_S_EMPLOYEE> Resources = DBContext.CM_S_EMPLOYEE.Where(E => EmployeeTypeCodes.Contains(E.EMPLOYEE_TYPE_CODE) && E.SHOP_CODE == shopCode && E.DT_START.GetValueOrDefault(DateTime.Today) <= DateTime.Today && E.DT_END.GetValueOrDefault(DateTime.Today) >= DateTime.Today && !bookedResources.Contains(E.EMPLOYEE_CODE));
			foreach (CM_S_EMPLOYEE resource in Resources)
				Result.Add(EntityMapper.Map<AppointmentResource, CM_S_EMPLOYEE>(DBContext, resource));

			return Result;
		}


		[HttpGet("[action]")]
		public ActionResult<IEnumerable<AvailabilitySlot>> GetDateRangeAvailableSlots(string shopCode, DateTime startDate, DateTime endDate, string serviceCode, short slotSize)
		{
			List<AvailabilitySlot> Result = new List<AvailabilitySlot>();
			string[] ScreeningEmployeeTypeCodes = { "SRO" };

			//Load screening service types
			string[] screeningServiceCodes = FoxDataService.GetScreeningServiceCodes(shopCode);

			CM_S_EMPLOYEE[] employees = null;
			List<AG_B_EMPLOYEE_WORKING_HOURS> workingHours = new List<AG_B_EMPLOYEE_WORKING_HOURS>();

			if (screeningServiceCodes.Contains(serviceCode.ToUpper())) //Screening
			{
				employees = DBContext.CM_S_EMPLOYEE.Where(E => E.SHOP_CODE == shopCode && ScreeningEmployeeTypeCodes.Contains(E.EMPLOYEE_TYPE_CODE) && (!E.DT_START.HasValue || E.DT_START.Value <= DateTime.Today) && (!E.DT_END.HasValue || E.DT_END.Value >= DateTime.Today)).ToArray();
			}
			else
				throw new ArgumentException("Service type other than screening are not yet implemented");

			if (employees != null)
			{
				string[] employeeCodes = employees.Select(E => E.EMPLOYEE_CODE).Distinct().ToArray();
				workingHours = DBContext.AG_B_EMPLOYEE_WORKING_HOURS.FromSql("EXECUTE [dbo].[p_AG_B_EMPLOYEE_WORKING_HOURS_GetListByEmployeeCodesAndDateRange] @COMPANY_CODE = {0}, @DIVISION_CODE = {1}, @EMPLOYEE_CODES = {2}, @StartDate = {3}, @EndDate = {4}, @SHOP_CODE={5}", Settings.Value.CompanyCode, Settings.Value.DivisionCode, string.Join("|", employeeCodes), startDate.Date, endDate.Date, shopCode).ToList();
			}

			while (startDate.Date <= endDate.Date)
			{
				IEnumerable<AvailabilitySlot> availabilitySlots = GetAvailableSlots(shopCode, employees, startDate, serviceCode, slotSize, workingHours);
				Result.AddRange(availabilitySlots);
				startDate = startDate.AddDays(1);
			}

			return Result;
		}

		List<AG_B_EMPLOYEE_WORKING_HOURS> GetEmployeeValidWorkingHours(string shopCode, IEnumerable<CM_S_EMPLOYEE> employees, DateTime date)
		{
			List<AG_B_EMPLOYEE_WORKING_HOURS> Result = new List<AG_B_EMPLOYEE_WORKING_HOURS>();
			if (employees != null)
			{
				string[] employeeCodes = employees.Select(E => E.EMPLOYEE_CODE).Distinct().ToArray();
				foreach (string employeeCode in employeeCodes)
				{
					AG_B_EMPLOYEE_WORKING_HOURS lastDate = DBContext.AG_B_EMPLOYEE_WORKING_HOURS.Where(E => E.SHOP_CODE == shopCode && E.EMPLOYEE_CODE == employeeCode && E.DT_VALID <= date.Date).OrderByDescending(E => E.DT_VALID).FirstOrDefault();
					if (lastDate != null)
					{
						AG_B_EMPLOYEE_WORKING_HOURS[] employeeWorkingHours = DBContext.AG_B_EMPLOYEE_WORKING_HOURS.Where(E => E.SHOP_CODE == shopCode && E.EMPLOYEE_CODE == employeeCode && E.DT_VALID == lastDate.DT_VALID).ToArray();
						Result.AddRange(employeeWorkingHours);
					}
				}
			}
			return Result;
		}

		[HttpGet("[action]")]
		public ActionResult<IEnumerable<AvailabilitySlot>> GetAvailableSlots(string shopCode, DateTime date, string serviceCode, short slotSize)
		{
			string[] ScreeningEmployeeTypeCodes = { "SRO" };

			//Load screening service types
			string[] screeningServiceCodes = FoxDataService.GetScreeningServiceCodes(shopCode);

			CM_S_EMPLOYEE[] employees = null;

			if (screeningServiceCodes.Contains(serviceCode.ToUpper())) //Screening
			{
				employees = DBContext.CM_S_EMPLOYEE.Where(E => E.SHOP_CODE == shopCode && ScreeningEmployeeTypeCodes.Contains(E.EMPLOYEE_TYPE_CODE) && (!E.DT_START.HasValue || E.DT_START.Value <= DateTime.Today) && (!E.DT_END.HasValue || E.DT_END.Value >= DateTime.Today)).ToArray();		
			}
			else
				throw new ArgumentException("Service type other than screening are not yet implemented");

			List<AG_B_EMPLOYEE_WORKING_HOURS> workingHours = GetEmployeeValidWorkingHours(shopCode, employees, date);
			List<AvailabilitySlot> Result = GetAvailableSlots(shopCode, employees, date, serviceCode, slotSize, workingHours);
			return Result;
		}



		protected List<AvailabilitySlot> GetAvailableSlots(string shopCode, IEnumerable<CM_S_EMPLOYEE> employees, DateTime date, string serviceCode, short slotSize, IEnumerable<AG_B_EMPLOYEE_WORKING_HOURS> workingHours)
		{
			const string defaultDescription = "Available";

			if (shopCode == null)
				throw new ArgumentNullException(nameof(shopCode), "shopCode cannot be null");
			if (serviceCode	 == null)
				throw new ArgumentNullException(nameof(serviceCode), "ServiceCode cannot be null");
			if (employees == null)
				throw new ArgumentNullException(nameof(employees), "employees cannot be null");
			if (workingHours == null)
				throw new ArgumentNullException(nameof(workingHours), "workingHours cannot be null");

			//Load special availability types
			string[] specialAvailabilityServiceCodes = FoxDataService.GetSpecialAvailabilityServiceCodes(shopCode);
			string[] unavailabilityServiceCodes = FoxDataService.GetUnavailabilityServiceCodes(shopCode);

			List<AvailabilitySlot> Result = new List<AvailabilitySlot>();
			AG_S_SERVICE service = DBContext.AG_S_SERVICE.FirstOrDefault(E => E.SERVICE_CODE == serviceCode);
			if (service == null)
				throw new ArgumentException($"No service found for service code: {serviceCode}");

			if (employees.Any())
			{
				List<AvailabilitySlot> availableSlots = new List<AvailabilitySlot>();

				//Add slots for each employee
				foreach (CM_S_EMPLOYEE employee in employees)
				{
					foreach (AG_B_EMPLOYEE_WORKING_HOURS workingHour in workingHours.Where(E => E.EMPLOYEE_CODE == employee.EMPLOYEE_CODE && E.DT_VALID <= date && SlotHelper.BynaryCheck(E, date)).OrderByDescending(E => E.DT_VALID).OrderBy(E => E.START_HOUR))
					{
						//Create standard availability slots
						availableSlots.AddRange(SlotHelper.CreateSlots(date, workingHour.START_HOUR.GetValueOrDefault(), workingHour.END_HOUR.GetValueOrDefault(), slotSize, employee.EMPLOYEE_CODE, employee.EMPLOYEE_DESCR, defaultDescription, service.BACKGROUND_COLOR.GetValueOrDefault(), service.FOREGROUND_COLOR.GetValueOrDefault()));
					}
				}

				string[] employeeCodes = employees.Select(E => E.EMPLOYEE_CODE).Distinct().ToArray();

				//Add special availability
				AG_B_APPOINTMENT[] specials = DBContext.AG_B_APPOINTMENT.Where(E => E.APPOINTMENT_SHOP_CODE == shopCode && employeeCodes.Contains(E.EMPLOYEE_CODE) && AllowedAppointmentStatus.Contains(E.STATUS_CODE) && specialAvailabilityServiceCodes.Contains(E.AG_S_SERVICE.SERVICE_TYPE_CODE) && E.DT_APPOINTMENT.Date == date.Date).ToArray();
				foreach (AG_B_APPOINTMENT special in specials)
				{
					CM_S_EMPLOYEE employee = employees.FirstOrDefault(E => E.EMPLOYEE_CODE == special.EMPLOYEE_CODE);
					List<AvailabilitySlot> specialSlots = SlotHelper.CreateSlots(special.DT_APPOINTMENT.Date, special.DT_APPOINTMENT.TimeOfDay, special.DT_APPOINTMENT.AddMinutes(special.DURATION.GetValueOrDefault()).TimeOfDay, slotSize, employee?.EMPLOYEE_CODE, employee?.EMPLOYEE_DESCR, defaultDescription, service.BACKGROUND_COLOR.GetValueOrDefault(), service.FOREGROUND_COLOR.GetValueOrDefault());
					foreach (AvailabilitySlot slot in specialSlots)
						if (!availableSlots.Contains(slot))
							availableSlots.Add(slot);
				}
				//Get already booked slots
				AG_B_APPOINTMENT[] appointments = DBContext.AG_B_APPOINTMENT.Where(E => E.APPOINTMENT_SHOP_CODE == shopCode && AllowedAppointmentStatus.Contains(E.STATUS_CODE) && employeeCodes.Contains(E.EMPLOYEE_CODE) && !specialAvailabilityServiceCodes.Contains(E.AG_S_SERVICE.SERVICE_TYPE_CODE) && E.DT_APPOINTMENT.Date == date.Date).ToArray();
				//Get special unavailability
				AG_B_APPOINTMENT[] unavailabilties = DBContext.AG_B_APPOINTMENT.Where(E => E.APPOINTMENT_SHOP_CODE == shopCode && AllowedAppointmentStatus.Contains(E.STATUS_CODE) && employeeCodes.Contains(E.EMPLOYEE_CODE) && unavailabilityServiceCodes.Contains(E.AG_S_SERVICE.SERVICE_TYPE_CODE) && E.DT_APPOINTMENT.Date == date.Date).ToArray();
				List<AG_B_APPOINTMENT> busySlots = new List<AG_B_APPOINTMENT>(appointments);
				busySlots.AddRange(unavailabilties);
				//Remove busy slots
				if (busySlots.Any()) 
					foreach (AG_B_APPOINTMENT appointment in busySlots)
					{
						availableSlots.RemoveAll(E => E.ResourceId == appointment.EMPLOYEE_CODE &&
																		((appointment.DT_APPOINTMENT >= E.Start  && appointment.DT_APPOINTMENT < E.End)
																	|| (appointment.DT_APPOINTMENT.AddMinutes(appointment.DURATION.GetValueOrDefault()) > E.Start && appointment.DT_APPOINTMENT.AddMinutes(appointment.DURATION.GetValueOrDefault()) < E.End)
																	|| (appointment.DT_APPOINTMENT <= E.Start && appointment.DT_APPOINTMENT.AddMinutes(appointment.DURATION.GetValueOrDefault()) > E.End)));
					}

				if (employeeCodes.Any() && availableSlots.Any())
					Result.AddRange(availableSlots.OrderBy(E => E.ResourceId).OrderBy(E => E.Start).Distinct(new AvailabilitySlotComparer()));
				else
					Result.AddRange(availableSlots);
			}

			return Result;
		}

		/// <summary>
		/// Update and appointment to a new status
		/// </summary>
		/// <param name="rowGuid">Appointment rowguid</param>
		/// <param name="statusCode">new appointment status code</param>
		[HttpPut("[action]")]
		public void UpdateAppointmentStatus(Guid rowGuid, string statusCode)
		{
			AG_B_APPOINTMENT appointment = DBContext.AG_B_APPOINTMENT.FirstOrDefault(E => E.ROWGUID == rowGuid);
			if (appointment == null)
				throw new NotFoundException($"Cannot find appointment with RowGuid={rowGuid}");

			appointmentStatus.CheckNewStatusAllowed(appointment, statusCode);

			appointment.STATUS_CODE = statusCode;
			EntityMapper.UpdateEntityStandardFields(appointment);

			AG_B_APPOINTMENT_STATUS_HISTORY history = DBContext.AG_B_APPOINTMENT_STATUS_HISTORY.FirstOrDefault(E => E.ROWGUID == rowGuid);
			if (history != null)
			{
				if (statusCode == appointmentStatus.Confirmed)
				{
					history.CONFIRMATION_STATUS = statusCode;
					history.CONFIRMED_DATE = DateTime.Now;
					history.CONFIRMED_USER = appointment.USERUPDATE;
					EntityMapper.UpdateEntityStandardFields(history);
				}
				else if (statusCode == appointmentStatus.Deleted || statusCode == appointmentStatus.Rescheduled)
				{
					history.CONFIRMATION_STATUS = statusCode;
					history.CANCEL_DATE = DateTime.Now;
					history.CANCEL_USER = appointment.USERUPDATE;
					EntityMapper.UpdateEntityStandardFields(history);
				}
			}
			DBContext.SaveChanges();
		}

		[HttpPut("[action]")]
		public void ConfirmAppointment(Guid rowGuid)
		{
			UpdateAppointmentStatus(rowGuid, appointmentStatus.Confirmed);
		}

		[HttpPut("[action]")]
		public void DeleteAppointment(Guid rowGuid)
		{
			UpdateAppointmentStatus(rowGuid, appointmentStatus.Deleted);
		}

		[HttpGet("[action]")]
		public ActionResult<OperationResult<bool>> CanBookAppointment(string customerCode, string shopCode, string serviceCode, DateTime appointmentDate, Guid? rescheduledAppointmentRowGuid = null)
		{
			return CheckCanBookAppointment(customerCode, shopCode, serviceCode, appointmentDate, rescheduledAppointmentRowGuid);
		}

		private OperationResult<bool> CheckCanBookAppointment(string customerCode, string shopCode, string serviceCode, DateTime appointmentDate, Guid? rescheduledAppointmentRowGuid = null)
		{
			if (string.IsNullOrWhiteSpace(customerCode))
				throw new ArgumentNullException(nameof(customerCode), "CustomerCode cannot be null");
			if (string.IsNullOrWhiteSpace(shopCode))
				throw new ArgumentNullException(nameof(shopCode), "ShopCode cannot be null");
			if (string.IsNullOrWhiteSpace(serviceCode))
				throw new ArgumentNullException(nameof(serviceCode), "ServiceCode cannot be null");
			if (appointmentDate == DateTime.MinValue)
				throw new ArgumentNullException(nameof(appointmentDate), "AppointmentDate cannot be null");

			OperationResult<bool> Result = new OperationResult<bool>() { Result = true, Message = "OK" };
			string[] screeningServiceCodes = FoxDataService.GetScreeningServiceCodes(shopCode);

			if (screeningServiceCodes.Contains(serviceCode))
			{
				string[] statusList = { appointmentStatus.Arrived, /* appointmentStatus.Completed, */ appointmentStatus.Open, appointmentStatus.Confirmed };
				string[] failedCompletedOutcomeList = { "009" /* Failed To Attend (FTA)*/, "005" /* Attended No Service Provided */};
				var predicate = PredicateBuilder.New<AG_B_APPOINTMENT>(E => E.CUSTOMER_CODE == customerCode && screeningServiceCodes.Contains(E.SERVICE_CODE) && E.DT_APPOINTMENT.Date > appointmentDate.Date.AddMonths(-Settings.Value.ScreeningAppointmentMonthTolerance)
					&& (statusList.Contains(E.STATUS_CODE) || (E.STATUS_CODE == appointmentStatus.Completed && !failedCompletedOutcomeList.Contains(E.AG_B_APPOINTMENT_EXT_AUS.OUTCOME_CODE))));
				if (rescheduledAppointmentRowGuid.GetValueOrDefault(Guid.Empty) != Guid.Empty)
					predicate = predicate.And(E => E.ROWGUID != rescheduledAppointmentRowGuid);
				int appointmentCount = DBContext.AG_B_APPOINTMENT.Count(predicate);
				if (appointmentCount > 0)
				{
					Result.Result = false;
					Result.Message = $"Screening appointment can be booked only every {Settings.Value.ScreeningAppointmentMonthTolerance} month(s)";
				}
			}

			return Result;
		}


		/// <summary>
		/// Retrieve clinicians with valid working hours on a specific shop/date/employee types
		/// </summary>
		/// <param name="shopCode">Shop code</param>
		/// <param name="date">Date</param>
		/// <param name="employeeTypeCodes">Employee type codes ( | separated list), defaulted to SRO if null </param>
		/// <returns></returns>
		[HttpGet("[action]")]
		public ActionResult<List<Clinician>> GetWorkingClinician(string shopCode, DateTime date, string employeeTypeCodes = null)
		{
			string[] employeeTypes = null;
			if (!string.IsNullOrWhiteSpace(employeeTypeCodes))
				employeeTypes = employeeTypeCodes.Split("|");

			List<Clinician> Result = new List<Clinician>();
			var predicate = PredicateBuilder.New<CM_S_EMPLOYEE>(true);
			if (employeeTypes == null)
				employeeTypes = new string[] { "SRO" }; //{ "SRO", "AUD", "CSO" };


			List<string> AllowedTypes = new List<string>();
			employeeTypes.ToList().ForEach(E => AllowedTypes.Add("'" + E + "'"));

			StringBuilder SQL = new StringBuilder("SELECT DISTINCT E.* FROM CM_S_EMPLOYEE E");
			SQL.Append(" JOIN AG_B_EMPLOYEE_WORKING_HOURS WH ON E.SHOP_CODE=WH.SHOP_CODE AND E.EMPLOYEE_CODE=WH.EMPLOYEE_CODE");
			SQL.Append($" WHERE E.SHOP_CODE IN ('*', '{shopCode}') AND WH.DT_VALID <= '{date:yyyy-MM-dd}' AND E.EMPLOYEE_TYPE_CODE IN (" + string.Join(", ", AllowedTypes) + ")");
			SQL.Append(" AND (E.DT_START IS NULL OR E.DT_START <= CAST(GETDATE() AS DATE)) AND (E.DT_END IS NULL OR E.DT_END >= CAST(GETDATE() AS DATE))");

			List<CM_S_EMPLOYEE> employees = DBContext.CM_S_EMPLOYEE.FromSql(SQL.ToString()).ToList();
			List<AG_B_EMPLOYEE_WORKING_HOURS> workingHours = GetEmployeeValidWorkingHours(shopCode, employees, date);
			foreach (CM_S_EMPLOYEE item in employees)
			{
				if (workingHours.Any(E => E.EMPLOYEE_CODE == item.EMPLOYEE_CODE && SlotHelper.BynaryCheck(E, date)))
				{
					Clinician model = EntityMapper.Map<Clinician, CM_S_EMPLOYEE>(DBContext, item);
					Result.Add(model);
				}
			}

			return Result;
		}

	}
}
