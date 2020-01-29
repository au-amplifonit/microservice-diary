using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITools.Models;

namespace Fox.Microservices.Diary.Models
{
	public class AvailabilitySlot
	{
		public string Title { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public TimeSpan Duration { get; set; }
		public string BackgroundColor { get; set; }
		public string TextColor { get; set; }
		public string ResourceId { get; set; }
		public string ResourceDescription { get; set; }
	}

	internal class AvailabilitySlotComparer : IEqualityComparer<AvailabilitySlot>
	{
		public bool Equals(AvailabilitySlot x, AvailabilitySlot y)
		{
			return x.Start == y.Start && x.End == y.End;
		}

		public int GetHashCode(AvailabilitySlot obj)
		{
			return string.Format("{0}-{1}", obj.Start, obj.End).GetHashCode(StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
