using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_B_APPOINTMENT_STATUS_HISTORY
    {
        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string LAPTOP_CODE { get; set; }
        public string APPOINTMENT_ID { get; set; }
        public string CONFIRMATION_STATUS { get; set; }
        public string REASON_CODE { get; set; }
        public DateTime? CONFIRMED_DATE { get; set; }
        public string CONFIRMED_USER { get; set; }
        public DateTime? CANCEL_DATE { get; set; }
        public string CANCEL_USER { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }
        public string CANCELLED_FROM_VC { get; set; }
    }
}
