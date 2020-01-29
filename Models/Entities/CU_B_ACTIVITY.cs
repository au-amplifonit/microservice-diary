using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class CU_B_ACTIVITY
    {
        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string LAPTOP_CODE { get; set; }
        public int ACTIVITY_ID { get; set; }
        public DateTime ACTIVITY_DATE { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string ACTIVITY_TYPE_CODE { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string CAMPAIGN_CODE { get; set; }
        public string PROMOTER_CODE { get; set; }
        public string PHYSICIAN_MG_CODE { get; set; }
        public string PHYSICIAN_ORL_CODE { get; set; }
        public string LOCATION_TYPE_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public string MEDIATYPE_CODE { get; set; }
        public string DAYCENTER_CODE { get; set; }
        public string NOTE { get; set; }
        public DateTime? REFERENCE_DATE { get; set; }
        public string REFERENCE_NUMBER { get; set; }
        public string TESTRESULT_LEFT { get; set; }
        public string TESTRESULT_RIGHT { get; set; }
        public string APPOINTMENT_ID { get; set; }
        public string RESULT_CODE { get; set; }
        public string REASON_CODE { get; set; }
        public DateTime? DT_EXAM { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }
        public string RECOMMENDEDBY_CODE { get; set; }
        public byte[] activity_rv { get; set; }

        public virtual CM_S_CAMPAIGN CM_S_CAMPAIGN { get; set; }
        public virtual CM_S_MEDIATYPE CM_S_MEDIATYPE { get; set; }
        public virtual CU_B_ADDRESS_BOOK CU_B_ADDRESS_BOOK { get; set; }
        public virtual CU_B_ADDRESS_BOOK CU_B_ADDRESS_BOOKNavigation { get; set; }
        public virtual SY_LOCATION_TYPE SY_LOCATION_TYPE { get; set; }
    }
}
