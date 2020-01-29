using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_S_APPOINTMENT_PLAN_DETAIL
    {
        public AG_S_APPOINTMENT_PLAN_DETAIL()
        {
            AG_B_APPOINTMENT = new HashSet<AG_B_APPOINTMENT>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string PLAN_CODE { get; set; }
        public short PLAN_APPOINTMENT_ORDER { get; set; }
        public short? INTERVAL_DAYS { get; set; }
        public short? TO_FIND_DAYS { get; set; }
        public string SERVICE_CODE { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual AG_S_APPOINTMENT_PLAN_HEADER AG_S_APPOINTMENT_PLAN_HEADER { get; set; }
        public virtual ICollection<AG_B_APPOINTMENT> AG_B_APPOINTMENT { get; set; }
    }
}
