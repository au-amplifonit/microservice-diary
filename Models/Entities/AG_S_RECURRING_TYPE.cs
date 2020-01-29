using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_S_RECURRING_TYPE
    {
        public AG_S_RECURRING_TYPE()
        {
            AG_S_RECURRING = new HashSet<AG_S_RECURRING>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string RECURRING_TYPE_CODE { get; set; }
        public string RECURRING_TYPE_DESCR { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual ICollection<AG_S_RECURRING> AG_S_RECURRING { get; set; }
    }
}
