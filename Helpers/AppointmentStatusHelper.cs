using Fox.Microservices.CommonUtils;
using Fox.Microservices.Diary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fox.Microservices.Diary.Helpers
{
	public class AppointmentStatusHelper
	{
		public string Open = "A";
		/// <summary>
		/// Closed status code
		/// </summary>
		public string Closed = "C";
		/// <summary>
		/// Deleted status code
		/// </summary>
		public string Deleted = "X";
		/// <summary>
		/// Not completed status code
		/// </summary>
		public string NotCompleted = "N";

		/// <summary>
		/// Completed status code
		/// </summary>
		public string Completed = "CP";
		/// <summary>
		/// Rescheduled status code
		/// </summary>
		public string Rescheduled = "R";
		/// <summary>
		/// Check-in status code
		/// </summary>
		public string CheckIn = "K";
		/// <summary>
		/// Error status code
		/// </summary>
		public string Error = "E";
		/// <summary>
		/// Confirmed status code
		/// </summary>
		public string Confirmed = "D";

		public string Unconfirmed = "A"; // CHANGED in A for CR NP-3642

		public string Workflow = "W";

		public string WorkflowRejected = "RE";

		/// <summary>
		/// Arrived status code
		/// </summary>
		public string Arrived = "C";

		public AppointmentStatusHelper(IFoxDataService foxDataService)
		{
			Open = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_StatoAperto", "A");
			Closed = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_StatoChiuso", "C");
			Deleted = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_StatoCancellato", "X");
			NotCompleted = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_StatoNonCompletato", "N");
			Completed = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_STATOCOMPLETATO", "CP");
			Rescheduled = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_StatoRischedulato", "R");
			CheckIn = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_StatoCheckIn", "K");
			Error = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_StatoError", "E");
			Confirmed = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_StatusConfirmed", "D");
			Unconfirmed = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_STATUSUNCONFIRMED", "A"); // CHANGED in A for CR NP-3642
			Workflow = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_StatusWorkflow", "W");
			WorkflowRejected = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_StatusWorkflowRejected", "RE");
			Arrived = foxDataService.GetGlobalParameterValue<string>("AG_B_APPOINTMENT_StatusArrived", "C");
		}

		public void CheckNewStatusAllowed(AG_B_APPOINTMENT appointment, string newStatus)
		{
			string oldStatus = appointment.STATUS_CODE;

			if (oldStatus == Open && (newStatus != Confirmed && newStatus != Deleted && newStatus != Rescheduled))
				throw new InvalidOperationException("Invalid status for an open appointment");
			else if (oldStatus == Rescheduled && newStatus != Rescheduled)
				throw new InvalidOperationException("Cannot change status on a rescheduled appointment");
			else if (oldStatus == Arrived && (newStatus != Completed && newStatus != Deleted))
				throw new InvalidOperationException("Invalid status for an arrived appointment");
			else if (newStatus == Deleted && appointment.DT_APPOINTMENT < DateTime.Today)
				throw new InvalidOperationException("Cannot delete past appointments");
		}
	}
}
