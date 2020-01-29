using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class CU_B_ADDRESS_BOOK
    {
        public CU_B_ADDRESS_BOOK()
        {
            AG_B_APPOINTMENT_EXT_AUS = new HashSet<AG_B_APPOINTMENT_EXT_AUS>();
            CU_B_ACTIVITYCU_B_ADDRESS_BOOK = new HashSet<CU_B_ACTIVITY>();
            CU_B_ACTIVITYCU_B_ADDRESS_BOOKNavigation = new HashSet<CU_B_ACTIVITY>();
            CU_B_ACTIVITY_EXT_AUS = new HashSet<CU_B_ACTIVITY_EXT_AUS>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string LASTNAME { get; set; }
        public string OTHERCONTACT_FIRSTNAME { get; set; }
        public string OTHERCONTACT_LASTNAME { get; set; }
        public string RELATION { get; set; }
        public string GENDER_CODE { get; set; }
        public string LANGUAGE_CODE { get; set; }
        public string COUNTRY_CODE { get; set; }
        public DateTime? BIRTHDATE { get; set; }
        public string BIRTHLOCATION { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string TAX_ID_NUMBER { get; set; }
        public string TITLE_CODE { get; set; }
        public string SALUTATION_CODE { get; set; }
        public string FLG_PRIVACYPERSDATA { get; set; }
        public string FLG_SENSDATA { get; set; }
        public string FLG_ADVERTISING { get; set; }
        public string FLG_PROFILING { get; set; }
        public DateTime? DT_PRIVACY_HQ_VALIDATION { get; set; }
        public string DUPLICATE_CODE { get; set; }
        public string STATUS_CODE { get; set; }
        public DateTime? DT_UPDATE_STATUS { get; set; }
        public string CATEGORY_CODE { get; set; }
        public string CUSTOMER_TYPE_CODE { get; set; }
        public DateTime? DT_UPDATE_CUSTOMERTYPE { get; set; }
        public string DISABILITY_CODE { get; set; }
        public string OCCUPATION_CODE { get; set; }
        public string OCCUPATION_OTHER { get; set; }
        public string FLG_RETIRED { get; set; }
        public string DEFAULT_PRICELIST_CODE { get; set; }
        public string FLG_TAX_EXEMPT { get; set; }
        public string FLG_NON_HA_PURCHASE { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual CU_B_ADDRESS_BOOK_EXT_AUS CU_B_ADDRESS_BOOK_EXT_AUS { get; set; }
        public virtual ICollection<AG_B_APPOINTMENT_EXT_AUS> AG_B_APPOINTMENT_EXT_AUS { get; set; }
        public virtual ICollection<CU_B_ACTIVITY> CU_B_ACTIVITYCU_B_ADDRESS_BOOK { get; set; }
        public virtual ICollection<CU_B_ACTIVITY> CU_B_ACTIVITYCU_B_ADDRESS_BOOKNavigation { get; set; }
        public virtual ICollection<CU_B_ACTIVITY_EXT_AUS> CU_B_ACTIVITY_EXT_AUS { get; set; }
    }
}
