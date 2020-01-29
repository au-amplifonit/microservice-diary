using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class CM_S_REFERENCE_SOURCE_EXT_AUS
    {
        public CM_S_REFERENCE_SOURCE_EXT_AUS()
        {
            InverseCM_S_REFERENCE_SOURCE_EXT_AUSNavigation = new HashSet<CM_S_REFERENCE_SOURCE_EXT_AUS>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string TYPE_CATEGORY_CODE { get; set; }
        public string CUSTOMER_TYPE_CODE { get; set; }
        public string REF_CODE { get; set; }
        public string REF_TYPE_CATEGORY_CODE { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }
        public int? SORT_ORDER { get; set; }

        public virtual CM_S_REFERENCE_SOURCE_EXT_AUS CM_S_REFERENCE_SOURCE_EXT_AUSNavigation { get; set; }
        public virtual ICollection<CM_S_REFERENCE_SOURCE_EXT_AUS> InverseCM_S_REFERENCE_SOURCE_EXT_AUSNavigation { get; set; }
    }
}
