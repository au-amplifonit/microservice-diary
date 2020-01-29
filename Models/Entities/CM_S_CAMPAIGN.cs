using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class CM_S_CAMPAIGN
    {
        public CM_S_CAMPAIGN()
        {
            CU_B_ACTIVITY = new HashSet<CU_B_ACTIVITY>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string CAMPAIGN_CODE { get; set; }
        public string CAMPAIGN_DESCR { get; set; }
        public string CAMPAIGN_TYPE_CODE { get; set; }
        public string STATUS_CODE { get; set; }
        public DateTime? DT_UPDATE_STATUS { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual ICollection<CU_B_ACTIVITY> CU_B_ACTIVITY { get; set; }
    }
}
