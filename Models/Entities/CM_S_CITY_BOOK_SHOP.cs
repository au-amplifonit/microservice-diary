using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class CM_S_CITY_BOOK_SHOP
    {
        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string AREA_CODE { get; set; }
        public short CITY_COUNTER { get; set; }
        public string ZIP_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public DateTime DT_START { get; set; }
        public DateTime DT_END { get; set; }
        public string FLG_PREFERRED { get; set; }
        public string FLG_MAIN { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }
    }
}
