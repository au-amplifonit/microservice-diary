using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_B_EMPLOYEE_WORKING_HOURS
    {
        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public int AGENDA_TIME_ID { get; set; }
        public string LOCATION_TYPE_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public DateTime DT_VALID { get; set; }
        public int? MASK_DAY { get; set; }
        public int? MASK_WEEK { get; set; }
        public int? MASK_MONTH { get; set; }
        public TimeSpan? START_HOUR { get; set; }
        public TimeSpan? END_HOUR { get; set; }
        public int? BACKGROUND_COLOR { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual CM_S_EMPLOYEE CM_S_EMPLOYEE { get; set; }
    }
}
