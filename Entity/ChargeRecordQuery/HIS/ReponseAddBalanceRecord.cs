using System;

namespace GetDeptQuery_LY.Entity.ChargeRecordQuery.HIS
{
	// Token: 0x020000AE RID: 174
	public class ReponseAddBalanceRecord
	{
		// Token: 0x04000321 RID: 801
		public int Result = 0;

		// Token: 0x04000322 RID: 802
		public string Err = string.Empty;

		// Token: 0x04000323 RID: 803
		public PrePayRecordList[] PrePayRecordLists = null;
	}
}
