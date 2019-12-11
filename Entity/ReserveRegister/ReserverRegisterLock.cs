using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetDeptQuery_LY.Entity.ReserveRegister.ReserverRegisterLock.YQ
{
    public class request
    {
        public string CardNo = string.Empty;
        public string SchemaID = string.Empty;
        public string TimepointSortId = string.Empty;
        public string SourceType = string.Empty;
        public string UserID = string.Empty;
        public string FlowNo = string.Empty;
    }
    public class returnresult
    {
        public int returncode = 0;

        public string errormsg = "";
    }
    public class data
    {
        public string OrderNO = "";
    }
    public class response
    {
        public returnresult returnresult = new returnresult();

        public data data = new data();
    }
}

namespace GetDeptQuery_LY.Entity.ReserveRegister.ReserverRegisterLock.HIS
{
    public class RequestRegBooking
    {
        public string CardNo = string.Empty;
        public string SchemaID = string.Empty;
        public string TimepointSortId = string.Empty;
        public string SourceType = string.Empty;
        public string UserID = string.Empty;
        public string FlowNo = string.Empty;
    }
    public class ResultInfo
    {
        public int Result = -1;
        public string Err = string.Empty;
    }
}
