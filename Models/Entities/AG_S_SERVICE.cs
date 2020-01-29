using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_S_SERVICE
    {
        public AG_S_SERVICE()
        {
            AG_B_APPOINTMENT = new HashSet<AG_B_APPOINTMENT>();
            AG_B_APPOINTMENT_EXT_AUS = new HashSet<AG_B_APPOINTMENT_EXT_AUS>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string SERVICE_DESCR { get; set; }
        public string SERVICE_TYPE_CODE { get; set; }
        public short? DEFAULT_DURATION { get; set; }
        public int? BACKGROUND_COLOR { get; set; }
        public int? FOREGROUND_COLOR { get; set; }
        public int? OTHER_COLOR { get; set; }
        public string FLG_NEWJOURNEY { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual AG_S_SERVICE_TYPE AG_S_SERVICE_TYPE { get; set; }
        public virtual ICollection<AG_B_APPOINTMENT> AG_B_APPOINTMENT { get; set; }
        public virtual ICollection<AG_B_APPOINTMENT_EXT_AUS> AG_B_APPOINTMENT_EXT_AUS { get; set; }
    }
}
