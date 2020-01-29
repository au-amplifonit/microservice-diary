using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class SY_LOCATION_TYPE
    {
        public SY_LOCATION_TYPE()
        {
            AG_B_APPOINTMENT = new HashSet<AG_B_APPOINTMENT>();
            CU_B_ACTIVITY = new HashSet<CU_B_ACTIVITY>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string LOCATION_TYPE_CODE { get; set; }
        public string LOCATION_TYPE_DESCR { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual ICollection<AG_B_APPOINTMENT> AG_B_APPOINTMENT { get; set; }
        public virtual ICollection<CU_B_ACTIVITY> CU_B_ACTIVITY { get; set; }
    }
}
