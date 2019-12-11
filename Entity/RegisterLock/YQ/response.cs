using System;

namespace GetDeptQuery_LY.Entity.RegisterLock.YQ
{
    // Token: 0x02000052 RID: 82
    public class response
    {
        // Token: 0x04000186 RID: 390
        public returnresult returnresult = new returnresult();

        public data data = new data();
    }

    public class data {
        public string RegisterID = "";

        // Token: 0x040006CA RID: 1738
        public decimal PreferentialFee = 0m;

        // Token: 0x040006CB RID: 1739
        public string YBTranMsg = "";

        // Token: 0x040006CC RID: 1740
        public string Ext1 = "";

        // Token: 0x040006CD RID: 1741
        public string Ext2 = "";

        // Token: 0x040006CE RID: 1742
        public string Ext3 = "";
    }
}
