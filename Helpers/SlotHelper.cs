using Fox.Microservices.Diary.Models;
using Fox.Microservices.Diary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fox.Microservices.Diary.Helpers
{
	public class SlotHelper
	{
		public static List<AvailabilitySlot> CreateSlots(DateTime date, TimeSpan startTime, TimeSpan endTime, short slotSize, string resourceId, string resourceDescription, string description = "Available", int backgorundColor = 0, int foregroundColor = 0)
		{
			DateTime startDate = date.Date.Add(startTime);
			DateTime endDate = date.Date.Add(endTime);
			return CreateSlots(startDate, endDate, slotSize, resourceId, resourceDescription, description, backgorundColor, foregroundColor);
		}

		public static List<AvailabilitySlot> CreateSlots(DateTime startDate, DateTime endDate, short slotSize, string resourceId, string resourceDescription, string description = "Available", int backgorundColor = 0, int foregroundColor = 0)
		{
			if (slotSize <= 0)
				throw new ArgumentException("Slot size must be greater than zero");
			if (string.IsNullOrWhiteSpace(resourceId))
				throw new ArgumentNullException("ResourceId cannot be null");

			List<AvailabilitySlot> Result = new List<AvailabilitySlot>();
			TimeSpan duration = new TimeSpan(0, slotSize, 0);


			while (startDate.Add(duration) <= endDate)
			{
				if (startDate >= DateTime.Today)
					Result.Add(new AvailabilitySlot
					{
						ResourceId = resourceId,
						ResourceDescription = resourceDescription,
						Title = description,
						Start = startDate,
						End = startDate.Add(duration),
						Duration = duration,				
						BackgroundColor = string.Format("#{0:X6}", backgorundColor),
						TextColor = string.Format("#{0:X6}", foregroundColor),
					});
				startDate = startDate.Add(duration);
			}

			return Result;
		}

		public static bool BynaryCheck(AG_B_EMPLOYEE_WORKING_HOURS workingHour, DateTime date)
		{
			bool IsLastOfMonth = false;
			if (date.Date.AddDays(7).Month != date.Date.Month)
				IsLastOfMonth = true;

			int iDay = GetDayMask(date);

			int iSett = GetWeekMask(date);

			int iMonth = GetMonthMask(date);

			return (workingHour.MASK_DAY & iDay) == iDay 
				&& ((workingHour.MASK_WEEK & iSett) == iSett || (IsLastOfMonth && (workingHour.MASK_WEEK & 16) == 16)) 
				&& (workingHour.MASK_MONTH & iMonth) == iMonth;
		}


		public static int GetDayMask(DateTime date)
		{
			// Giorno  -- 1 Linedì 7 Domenica
			int iDay = (int)date.DayOfWeek;
			if (iDay == 0) iDay = 7;

			return (int)Math.Pow(2, (iDay - 1));
		}

		public static int GetWeekMask(DateTime date)
		{
			// Settimana -- Significa i giorni 1-7, 8-14
			int iSett = ((date.Day - 1) / 7) + 1;

			return (int)Math.Pow(2, (iSett - 1));
		}

		public static int GetMonthMask(DateTime date)
		{
			// Mese
			int iMonth = date.Month;

			return (int)Math.Pow(2, (iMonth - 1));
		}
	}
}
