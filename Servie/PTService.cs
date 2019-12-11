using System;
using GetDeptQuery_LY.Action;

namespace GetDeptQuery_LY.Servie
{
	// Token: 0x0200003C RID: 60
	public class PTService
	{
		
        public string MOP_OutpRegisterDeptQuery_LY(string InXml)
        {
            return this.registerService.MOP_OutpRegisterDeptQuery_LY(InXml);
        }

        public string MOP_OutpReserverDeptQuery_LY(string InXml)
        {
            return this.registerService.MOP_OutpReserverDeptQuery_LY(InXml);
        }

        //MOP_RegisterGetDoctorSchedule
        public string MOP_RegisterGetDoctorSchedule(string InXml)
        {
            return this.registerService.MOP_RegisterGetDoctorSchedule(InXml);
        }

        //MOP_OutpRegisterLock
        public string MOP_OutpRegisterLock(string InXml)
        {
            return this.registerService.MOP_OutpRegisterLock(InXml);
        }

        //MOP_OutpDoctorQuery_LY
        public string MOP_OutpDoctorQuery_LY(string InXml)
        {
            return this.registerService.MOP_OutpDoctorQuery_LY(InXml);
        }

        public string AddHisRegisterCostSettlement(string InXml)
        {
            return this.registerService.AddHisRegisterCostSettlement(InXml);
        }
        // Token: 0x0400011B RID: 283
        private RegisterAction registerService = new RegisterAction();
	}
}
