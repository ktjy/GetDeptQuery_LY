using System;

namespace GetDeptQuery_LY.Entity.InHosChargeInfo.HIS
{
	// Token: 0x02000079 RID: 121
	public class RequestPrePay
	{
		// Token: 0x0400020F RID: 527
		public string CardNo = string.Empty;

		// Token: 0x04000210 RID: 528
		public string InpatientNo = string.Empty;

		// Token: 0x04000211 RID: 529
		public decimal Amt = 0.00m;

		// Token: 0x04000212 RID: 530
		public string PayMode = "";

		// Token: 0x04000213 RID: 531
		public string BankNo = string.Empty;

		// Token: 0x04000214 RID: 532
		public string BankName = string.Empty;

		// Token: 0x04000215 RID: 533
		public string FlowNo = string.Empty;

		// Token: 0x04000216 RID: 534
		public string PowerPayChannel = string.Empty;

		// Token: 0x04000217 RID: 535
		public string UserID = string.Empty;

		// Token: 0x04000218 RID: 536
		public int PayType = 0;
	}
}
