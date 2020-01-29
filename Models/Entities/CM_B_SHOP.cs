using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class CM_B_SHOP
    {
        public CM_B_SHOP()
        {
            AG_S_APPOINTMENT_PLAN_HEADER = new HashSet<AG_S_APPOINTMENT_PLAN_HEADER>();
            CU_B_ACTIVITY_EXT_AUS = new HashSet<CU_B_ACTIVITY_EXT_AUS>();
            CU_B_ADDRESS_BOOK_EXT_AUS = new HashSet<CU_B_ADDRESS_BOOK_EXT_AUS>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string SHOP_DESCR { get; set; }
        public string LEGAL_DESCR { get; set; }
        public string SHOP_TYPE_CODE { get; set; }
        public string OBJ_CODE { get; set; }
        public string ORGANIZATION_CODE { get; set; }
        public string EXTRA_INFO { get; set; }
        public string FLG_ACTIVE { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual ICollection<AG_S_APPOINTMENT_PLAN_HEADER> AG_S_APPOINTMENT_PLAN_HEADER { get; set; }
        public virtual ICollection<CU_B_ACTIVITY_EXT_AUS> CU_B_ACTIVITY_EXT_AUS { get; set; }
        public virtual ICollection<CU_B_ADDRESS_BOOK_EXT_AUS> CU_B_ADDRESS_BOOK_EXT_AUS { get; set; }
    }
}
