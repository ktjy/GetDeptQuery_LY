using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetDeptQuery_LY.Entity.RegisterLock.HIS
{
    public class RequestReging
    {
        //042 现场分时段挂号锁号释号
        public string CardNo = string.Empty;
        public string SchemaID = string.Empty;
        public string TimepointSortId = string.Empty;
        public string TypeCode = string.Empty;//1 锁号 2释号
        public string UserID = string.Empty;
        public string FlowNo = string.Empty;
    }
}