using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using GetDeptQuery_LY.Properties;

namespace GetDeptQuery_LY.HisInterface
{
	// Token: 0x02000064 RID: 100
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Web.Services", "2.0.50727.8825")]
	[WebServiceBinding(Name = "ServiceSoap", Namespace = "http://tempuri.org/")]
	public class Service : SoapHttpClientProtocol
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00006528 File Offset: 0x00004728
		public Service()
		{
			this.Url = Settings.Default.GetDeptQuery_LY_HisInterface_Service; 

            if (this.IsLocalFileSystemWebService(this.Url))
			{
				this.UseDefaultCredentials = true;
				this.useDefaultCredentialsSetExplicitly = false;
			}
			else
			{
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00006580 File Offset: 0x00004780
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00006598 File Offset: 0x00004798
		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000065DC File Offset: 0x000047DC
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00002CF3 File Offset: 0x00000EF3
		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000BA RID: 186 RVA: 0x00002D05 File Offset: 0x00000F05
		// (remove) Token: 0x060000BB RID: 187 RVA: 0x00002D1E File Offset: 0x00000F1E
		public event ProcessCompletedEventHandler ProcessCompleted;

		// Token: 0x060000BC RID: 188 RVA: 0x000065F4 File Offset: 0x000047F4
		[SoapDocumentMethod("http://tempuri.org/Process", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string Process(string transCode, string xml)
		{
			object[] array = base.Invoke("Process", new object[]
			{
				transCode,
				xml
			});
			return (string)array[0];
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002D37 File Offset: 0x00000F37
		public void ProcessAsync(string transCode, string xml)
		{
			this.ProcessAsync(transCode, xml, null);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000662C File Offset: 0x0000482C
		public void ProcessAsync(string transCode, string xml, object userState)
		{
			if (this.ProcessOperationCompleted == null)
			{
				this.ProcessOperationCompleted = new SendOrPostCallback(this.OnProcessOperationCompleted);
			}
			base.InvokeAsync("Process", new object[]
			{
				transCode,
				xml
			}, this.ProcessOperationCompleted, userState);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006684 File Offset: 0x00004884
		private void OnProcessOperationCompleted(object arg)
		{
			if (this.ProcessCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ProcessCompleted(this, new ProcessCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00002D44 File Offset: 0x00000F44
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000066D4 File Offset: 0x000048D4
		private bool IsLocalFileSystemWebService(string url)
		{
			bool result;
			if (url == null || url == string.Empty)
			{
				result = false;
			}
			else
			{
				Uri uri = new Uri(url);
				result = (uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0);
			}
			return result;
		}

		// Token: 0x040001CA RID: 458
		private SendOrPostCallback ProcessOperationCompleted;

		// Token: 0x040001CB RID: 459
		private bool useDefaultCredentialsSetExplicitly;
	}
}
