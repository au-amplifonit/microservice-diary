using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class CU_B_ACTIVITY_EXT_AUS
    {
        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string LAPTOP_CODE { get; set; }
        public int ACTIVITY_ID { get; set; }
        public DateTime ACTIVITY_DATE { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string SUB_REASON_CODE { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }
        public DateTime? DT_APPOINTEMENT_FROM { get; set; }
        public DateTime? DT_APPOINTEMENT_TO { get; set; }

        public virtual CM_B_SHOP CM_B_SHOP { get; set; }
        public virtual CU_B_ADDRESS_BOOK CU_B_ADDRESS_BOOK { get; set; }
    }
}
