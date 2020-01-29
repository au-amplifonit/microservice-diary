using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_S_ROOM
    {
        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string ROOM_CODE { get; set; }
        public string ROOM_DESCR { get; set; }
        public string ROOM_TYPE_CODE { get; set; }
        public string OTHERS_INFO { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public DateTime? DT_END { get; set; }
        public string USERUPDATE { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public Guid ROWGUID { get; set; }
    }
}
