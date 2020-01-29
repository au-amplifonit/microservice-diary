using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_S_SERVICE_TYPE
    {
        public AG_S_SERVICE_TYPE()
        {
            AG_S_SERVICE = new HashSet<AG_S_SERVICE>();
        }

        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SERVICE_TYPE_CODE { get; set; }
        public string SERVICE_TYPE_DESCR { get; set; }
        public string FLG_ALLOW_APPOINTMENT { get; set; }
        public DateTime? DT_START { get; set; }
        public DateTime? DT_END { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual ICollection<AG_S_SERVICE> AG_S_SERVICE { get; set; }
    }
}
