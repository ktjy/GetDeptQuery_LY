using System;

namespace GetDeptQuery_LY.Entity.GetPatientInfo.HIS
{
	// Token: 0x02000007 RID: 7
	public class ReponsePatientInfo
	{
		// Token: 0x04000010 RID: 16
		public int Result = 0;

		// Token: 0x04000011 RID: 17
		public string Err = string.Empty;

		// Token: 0x04000012 RID: 18
		public AccountPatientInfo[] AccountPatientInfos = null;
	}
}
