using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LYZXYYWebService.Properties
{
	// Token: 0x02000010 RID: 16
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000036E8 File Offset: 0x000018E8
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00003700 File Offset: 0x00001900
		[ApplicationScopedSetting]
		[DefaultSettingValue("http://182.168.56.125/Service.asmx")]
		[SpecialSetting(SpecialSetting.WebServiceUrl)]
		[DebuggerNonUserCode]
		public string LYZXYYWebService_HisInterface_Service
		{
			get
			{
				return (string)this["LYZXYYWebService_HisInterface_Service"];
			}
		}

		// Token: 0x04000033 RID: 51
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
