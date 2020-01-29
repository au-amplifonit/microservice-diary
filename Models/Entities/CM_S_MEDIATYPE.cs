using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class CM_S_MEDIATYPE
    {
        public CM_S_MEDIATYPE()
        {
            CU_B_ACTIVITY = new HashSet<CU_B_ACTIVITY>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string MEDIATYPE_CODE { get; set; }
        public string MEDIATYPE_DESCR { get; set; }
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
