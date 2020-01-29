using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_S_RESULT
    {
        public AG_S_RESULT()
        {
            AG_B_APPOINTMENT = new HashSet<AG_B_APPOINTMENT>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string RESULT_CODE { get; set; }
        public string RESULT_DESCR { get; set; }
        public string FLG_NOAHTEST { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual ICollection<AG_B_APPOINTMENT> AG_B_APPOINTMENT { get; set; }
    }
}
