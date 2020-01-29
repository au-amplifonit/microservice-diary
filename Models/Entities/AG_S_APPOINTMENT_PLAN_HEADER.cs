using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_S_APPOINTMENT_PLAN_HEADER
    {
        public AG_S_APPOINTMENT_PLAN_HEADER()
        {
            AG_S_APPOINTMENT_PLAN_DETAIL = new HashSet<AG_S_APPOINTMENT_PLAN_DETAIL>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string PLAN_CODE { get; set; }
        public string PLAN_DESCR { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual CM_B_SHOP CM_B_SHOP { get; set; }
        public virtual ICollection<AG_S_APPOINTMENT_PLAN_DETAIL> AG_S_APPOINTMENT_PLAN_DETAIL { get; set; }
    }
}
