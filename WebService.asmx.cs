using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GetDeptQuery_LY.Properties;
using GetDeptQuery_LY.Servie;

namespace GetDeptQuery_LY
{
    /// <summary>
    /// WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string MOP_OutpRegisterDeptQuery_LY(string InXml)
        {
            return this.ptService.MOP_OutpRegisterDeptQuery_LY(InXml);
        }

        [WebMethod]
        public string MOP_OutpReserverDeptQuery_LY(string InXml)
        {
            return this.ptService.MOP_OutpReserverDeptQuery_LY(InXml);
        }
        //MOP_RegisterGetDoctorSchedule

        [WebMethod]
        public string MOP_RegisterGetDoctorSchedule(string InXml)
        {
            return this.ptService.MOP_RegisterGetDoctorSchedule(InXml);
        }

        //MOP_OutpRegisterLock
        [WebMethod]
        public string MOP_OutpRegisterLock(string InXml)
        {
            return this.ptService.MOP_OutpRegisterLock(InXml);
        }
        //MOP_OutpDoctorQuery_LY
        [WebMethod]
        public string MOP_OutpDoctorQuery_LY(string InXml)
        {
            return this.ptService.MOP_OutpDoctorQuery_LY(InXml);
        }
        [WebMethod]
        public string AddHisRegisterCostSettlement(string InXml)
        {
            return this.ptService.AddHisRegisterCostSettlement(InXml);
        }
        
        private PTService ptService = new PTService();
    }
}
