using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class CU_B_ADDRESS_BOOK_EXT_AUS
    {
        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string TYPE_CODE { get; set; }
        public string CURRENT_FILE_LOCATION { get; set; }
        public string CURRENT_LOCATION_TYPE_CODE { get; set; }
        public string PRACTITIONER_CODE { get; set; }
        public string PRACTICE_CODE { get; set; }
        public string PRIVACY_CONSENT_SIGNED { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }
        public string SHOP_CODE { get; set; }
        public string OLD_TYPE_CODE { get; set; }
        public string VALIDATION_STATUS_CODE { get; set; }
        public DateTime? DT_UPDATE_VALIDATION_STATUS { get; set; }
        public string IS_DVA { get; set; }
        public string Is_MCLRecieved { get; set; }
        public string PREFERREDNAME { get; set; }
        public string JOURNEY_STAGE_CODE { get; set; }
        public string ELIGIBILITY_CODE { get; set; }
        public string IS_TOPUP { get; set; }
        public string FLG_LOST_OHS_ELIGIBILITY { get; set; }
        public DateTime? DT_REGISTERED { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public DateTime? DT_ARCHIVE_REQUESTED { get; set; }
        public string SOURCE_CODE { get; set; }
        public string SUB_SOURCE_CODE { get; set; }
        public string REF_SOURCE_CODE { get; set; }
        public string FLG_ARCHIVE_REQUESTED { get; set; }
        public string FLG_MIGRATED_FROM_VC { get; set; }
        public string RELATION { get; set; }
        public string NOTES { get; set; }
        public string BPAY_CRN { get; set; }
        public string FLG_TO_CALL { get; set; }
        public string DVA_CLIENT_NUMBER { get; set; }
        public string PREV_ALD_DVA { get; set; }
        public string DVA_CARD_TYPE_CODE { get; set; }
        public string IDCALL { get; set; }
        public decimal? OUTCOMEID { get; set; }
        public DateTime? CONTACTED_ON { get; set; }

        public virtual CM_B_SHOP CM_B_SHOP { get; set; }
        public virtual CM_S_EMPLOYEE CM_S_EMPLOYEE { get; set; }
        public virtual CU_B_ADDRESS_BOOK CU_B_ADDRESS_BOOK { get; set; }
    }
}
