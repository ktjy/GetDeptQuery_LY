using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetDeptQuery_LY.Entity.ReserveRegister.ReserveRegisterTicketOut.YQ
{
    public class request
    {
        public string CardNo = "";
        public string BookId = "";
        /// <summary>
        /// 0 支付失败 1成功        
        /// /// </summary>
        public string PayState = "";
        public string UserID = "";
        public string FlowNo = "";
        /// <summary>
        /// WX,ZFB,PT
        /// </summary>
        public string PayType = "";
        public string PowerPayChannel = "";
        public string OrderNo = "";
    }
    public class returnresult
    {
        public int returncode = 0;

        public string errormsg = "";
    }
    public class data
    {
        public string Adress = "";
    }
    public class response
    {
        public returnresult returnresult = new returnresult();

        public data data = new data();
    }

}
namespace GetDeptQuery_LY.Entity.ReserveRegister.ReserveRegisterTicketOut.HIS
{
    public class RequestRegBooked
    {
        public string CardNo = "";
        public string BookId = "";
        /// <summary>
        /// 0 支付失败 1成功        
        /// /// </summary>
        public string PayState = "";
        public string UserID = "";
        public string FlowNo = "";
        /// <summary>
        /// WX,ZFB,PT
        /// </summary>
        public string PayType = "";
        public string PowerPayChannel = "";
        public string OrderNo = "";
    }
    public class ResultInfo
    {
        public int Result = -1;
        public string Err = "";
    }

}
