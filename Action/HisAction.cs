using System;
using GetDeptQuery_LY.HisInterface;

namespace GetDeptQuery_LY.Action
{
	// Token: 0x02000034 RID: 52
	public class HisAction
	{
        // Token: 0x0400010D RID: 269
        public static GetDeptQuery_LY.HisInterface.Service hisService = new GetDeptQuery_LY.HisInterface.Service();
        // Token: 0x0600005C RID: 92 RVA: 0x0000474C File Offset: 0x0000294C
        public static string Process(string TranCode, string InXml)
		{
			return hisService.Process(TranCode, InXml).Replace("utf-16", "utf-8");
		}

	
	}
}
