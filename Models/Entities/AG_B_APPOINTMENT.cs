using System;
using System.Collections.Generic;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class AG_B_APPOINTMENT
    {
        public string COMPANY_CODE { get; set; }
        public string DIVISION_CODE { get; set; }
        public string SHOP_CODE { get; set; }
        public string LAPTOP_CODE { get; set; }
        public string APPOINTMENT_ID { get; set; }
        public string STATUS_CODE { get; set; }
        public string REASON_CODE { get; set; }
        public DateTime DT_APPOINTMENT { get; set; }
        public int? DURATION { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string SERVICE_CODE { get; set; }
        public string RECURRING_CODE { get; set; }
        public string APPOINTMENT_SHOP_CODE { get; set; }
        public string LOCATION_TYPE_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public string RESULT_CODE { get; set; }
        public string ROOM_CODE { get; set; }
        public string NOTE { get; set; }
        public short? TIMEBEFORE { get; set; }
        public short? TIMEAFTER { get; set; }
        public string FLG_CONFIRMATION { get; set; }
        public string FLG_SMS { get; set; }
        public string FLG_EMAIL { get; set; }
        public string APP_JOURNEY_NUMBER { get; set; }
        public string PLAN_ID { get; set; }
        public string PLAN_CODE { get; set; }
        public short? PLAN_APPOINTMENT_ORDER { get; set; }
        public DateTime? DT_INSERT { get; set; }
        public string USERINSERT { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string USERUPDATE { get; set; }
        public Guid ROWGUID { get; set; }

        public virtual AG_S_APPOINTMENT_PLAN_DETAIL AG_S_APPOINTMENT_PLAN_DETAIL { get; set; }
        public virtual AG_S_RECURRING AG_S_RECURRING { get; set; }
        public virtual AG_S_RESULT AG_S_RESULT { get; set; }
        public virtual AG_S_SERVICE AG_S_SERVICE { get; set; }
        public virtual CM_S_EMPLOYEE CM_S_EMPLOYEE { get; set; }
        public virtual SY_LOCATION_TYPE SY_LOCATION_TYPE { get; set; }
        public virtual AG_B_APPOINTMENT_EXT_AUS AG_B_APPOINTMENT_EXT_AUS { get; set; }
    }
}
