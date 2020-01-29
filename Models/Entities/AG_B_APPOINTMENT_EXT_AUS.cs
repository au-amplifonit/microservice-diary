using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_B_APPOINTMENT_EXT_AUS
    {
        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string LAPTOP_CODE { get; set; }
        public string APPOINTMENT_ID { get; set; }
        public string SERVICE_CODE { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string SUB_REASON_CODE { get; set; }
        public string OUTCOME_CODE { get; set; }
        public string OUTCOME_REASON_CODE { get; set; }
        public string OUTCOME_SUBREASON_CODE { get; set; }
        public int? RESCHEDULED_NUMBER { get; set; }
        public string VOUCHER_NUMBER { get; set; }
        public string VOUCHER_TYPE { get; set; }
        public string INTERN_CODE { get; set; }
        public string FLG_ARRIVAL { get; set; }
        public string IS_MCLNEEDED { get; set; }
        public string OUTCOME_SUB_REASON_CODE { get; set; }
        public string BOOKEDONBEHALF_FIRSTNAME { get; set; }
        public string BOOKEDONBEHALF_LASTNAME { get; set; }
        public string BOOKEDONBEHALF_RELATIONSHIP { get; set; }
        public string BOOKEDONBEHALF_HOME_PHONE { get; set; }
        public string BOOKEDONBEHALF_MOBILE_PHONE { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }
        public string VALIDATION_STATUS_CODE { get; set; }
        public DateTime? DT_UPDATE_VALIDATION_STATUS { get; set; }
        public string LEAVE_SHOP_CODE { get; set; }
        public string LEAVE_ROOM_CODE { get; set; }
        public DateTime? DT_CREATED { get; set; }
        public string LEADID { get; set; }
        public string APPOINTMENT_SOURCE { get; set; }
        public string SOURCE_APPOINTMENT_ID { get; set; }
        public string SOURCE_PROMOTER_CODE { get; set; }
        public string SOURCE_TRACKING_ID { get; set; }
        public string CAMPAIGN_CODE { get; set; }
        public string MEDIATYPE_CODE { get; set; }

        public virtual AG_B_APPOINTMENT AG_B_APPOINTMENT { get; set; }
        public virtual AG_S_OUTCOME_EXT_AUS AG_S_OUTCOME_EXT_AUS { get; set; }
        public virtual AG_S_OUTCOME_REASON_EXT_AUS AG_S_OUTCOME_REASON_EXT_AUS { get; set; }
        public virtual AG_S_SERVICE AG_S_SERVICE { get; set; }
        public virtual CM_S_EMPLOYEE CM_S_EMPLOYEE { get; set; }
        public virtual CU_B_ADDRESS_BOOK CU_B_ADDRESS_BOOK { get; set; }
    }
}
