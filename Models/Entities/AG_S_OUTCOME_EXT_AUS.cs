using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_S_OUTCOME_EXT_AUS
    {
        public AG_S_OUTCOME_EXT_AUS()
        {
            AG_B_APPOINTMENT_EXT_AUS = new HashSet<AG_B_APPOINTMENT_EXT_AUS>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string OUTCOME_CODE { get; set; }
        public string OUTCOME_DESCR { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }
        public string ENTITY_TYPE_CODE { get; set; }

        public virtual ICollection<AG_B_APPOINTMENT_EXT_AUS> AG_B_APPOINTMENT_EXT_AUS { get; set; }
    }
}
