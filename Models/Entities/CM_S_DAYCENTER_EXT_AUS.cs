using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class CM_S_DAYCENTER_EXT_AUS
    {
        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string DAYCENTER_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string OHS_SITE_ID { get; set; }
        public string LOCATION_TYPE { get; set; }
        public DateTime DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }
    }
}
