using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetDeptQuery_LY.Entity.ReserveRegister.ReserveRegisterOrder.YQ
{
    public class request
    {
        public string BookId = string.Empty;
        public string UserID = string.Empty;
        public string FlowNo = string.Empty;
        public string PatientName = string.Empty;
    }
    public class returnresult
    {
        public int returncode = 0;

        public string errormsg = "";
    }
    public class data
    {
        public string BookId = string.Empty;
        public string Sex = string.Empty;
        public string BirthDay = string.Empty;
        public string BookDate = string.Empty;
        public string Dept = string.Empty;
        public string Doct = string.Empty;
        public string SchemaId = string.Empty;
        public decimal Cost = 0.00m;
        public string Tel = string.Empty;
        public string IDENO = string.Empty;
        public string SourceType = string.Empty;
        public string Oper = string.Empty;
        public string OperDate = string.Empty;
        public string OrderStatus = "2";
        public string StartTime = string.Empty;
        public string EndTime = string.Empty;
        public string PatientName = string.Empty;
        public decimal ClinicFee = 0.00m;


    }
    public class response
    {
        public returnresult returnresult = new returnresult();

        public data data = new data();
    }
}
namespace GetDeptQuery_LY.Entity.ReserveRegister.ReserveRegisterOrder.HIS
{
    public class RequestBookId
    {
        public string BookId = string.Empty;
        public string UserID = string.Empty;
        public string FlowNo = string.Empty;
    }
    public class ReponseRegBooking
    {
        public int Result = 0;
        public string Err = string.Empty;
        public string BookId = string.Empty;
        public string Sex = string.Empty;
        public string BirthDay = string.Empty;
        public string BookDate = string.Empty;
        public string Dept = string.Empty;
        public string Doct = string.Empty;
        public string SchemaId = string.Empty;
        public decimal Cost = 0.00m;
        public string Tel = string.Empty;
        public string IDENO = string.Empty;
        public string SourceType = string.Empty;
        public string Oper = string.Empty;
        public string OperDate = string.Empty;
    }
}
