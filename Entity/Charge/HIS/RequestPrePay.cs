using System;

namespace GetDeptQuery_LY.Entity.Charge.HIS
{
	// Token: 0x02000092 RID: 146
	public class RequestPrePay
	{
		// Token: 0x04000286 RID: 646
		public string CardNo = string.Empty;

		// Token: 0x04000287 RID: 647
		public decimal Amt = 0.00m;

		// Token: 0x04000288 RID: 648
		public string PayMode = "";

		// Token: 0x04000289 RID: 649
		public string BankNo = string.Empty;

		// Token: 0x0400028A RID: 650
		public string BankName = string.Empty;

		// Token: 0x0400028B RID: 651
		public string FlowNo = string.Empty;

		// Token: 0x0400028C RID: 652
		public string PowerPayChannel = string.Empty;

		// Token: 0x0400028D RID: 653
		public string UserID = string.Empty;

		// Token: 0x0400028E RID: 654
		public int PayType = 0;
	}
}
