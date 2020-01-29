using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class CM_S_EMPLOYEE
    {
        public CM_S_EMPLOYEE()
        {
            AG_B_APPOINTMENT = new HashSet<AG_B_APPOINTMENT>();
            AG_B_APPOINTMENT_EXT_AUS = new HashSet<AG_B_APPOINTMENT_EXT_AUS>();
            AG_B_EMPLOYEE_WORKING_HOURS = new HashSet<AG_B_EMPLOYEE_WORKING_HOURS>();
            CU_B_ADDRESS_BOOK_EXT_AUS = new HashSet<CU_B_ADDRESS_BOOK_EXT_AUS>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string EMPLOYEE_DESCR { get; set; }
        public string EMPLOYEE_TYPE_CODE { get; set; }
        public string EMPLOYEE_ID { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual SY_EMPLOYEE_TYPE SY_EMPLOYEE_TYPE { get; set; }
        public virtual ICollection<AG_B_APPOINTMENT> AG_B_APPOINTMENT { get; set; }
        public virtual ICollection<AG_B_APPOINTMENT_EXT_AUS> AG_B_APPOINTMENT_EXT_AUS { get; set; }
        public virtual ICollection<AG_B_EMPLOYEE_WORKING_HOURS> AG_B_EMPLOYEE_WORKING_HOURS { get; set; }
        public virtual ICollection<CU_B_ADDRESS_BOOK_EXT_AUS> CU_B_ADDRESS_BOOK_EXT_AUS { get; set; }
    }
}
