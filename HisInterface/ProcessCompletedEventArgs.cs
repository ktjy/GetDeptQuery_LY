using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace GetDeptQuery_LY.HisInterface
{
	// Token: 0x02000066 RID: 102
	[DebuggerStepThrough]
	[GeneratedCode("System.Web.Services", "2.0.50727.8825")]
	[DesignerCategory("code")]
	public class ProcessCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00002D4F File Offset: 0x00000F4F
		internal ProcessCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000673C File Offset: 0x0000493C
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x040001CD RID: 461
		private object[] results;
	}
}
