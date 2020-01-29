using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class SY_EMPLOYEE_TYPE
    {
        public SY_EMPLOYEE_TYPE()
        {
            CM_S_EMPLOYEE = new HashSet<CM_S_EMPLOYEE>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string EMPLOYEE_TYPE_CODE { get; set; }
        public string EMPLOYEE_TYPE_DESCR { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual ICollection<CM_S_EMPLOYEE> CM_S_EMPLOYEE { get; set; }
    }
}
