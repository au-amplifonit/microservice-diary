using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class SY_GENERAL_STATUS
    {
        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string STATUS_CODE { get; set; }
        public string ENTITY_TYPE_CODE { get; set; }
        public string STATUS_DESCR { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }
    }
}
