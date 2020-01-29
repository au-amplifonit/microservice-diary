using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_S_RECURRING
    {
        public AG_S_RECURRING()
        {
            AG_B_APPOINTMENT = new HashSet<AG_B_APPOINTMENT>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string RECURRING_CODE { get; set; }
        public string RECURRING_DESCR { get; set; }
        public short? RECURRING_COUNTER { get; set; }
        public string TYPE_CODE { get; set; }
        public int? FREQUENCY { get; set; }
        public string WEEKDAYS { get; set; }
        public int? DAYOFMONTH { get; set; }
        public int? MONTHOFYEAR { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual AG_S_RECURRING_TYPE AG_S_RECURRING_TYPE { get; set; }
        public virtual ICollection<AG_B_APPOINTMENT> AG_B_APPOINTMENT { get; set; }
    }
}
