using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.PCM.Enums
{
    public enum ContractCategory
    {
        [Description("五方主体")]
        FiveParty,

        [Description("中介服务")]
        IntermediaryService,

        [Description("虚拟合同(无合同支付)")]
        Virtual,

        [Description("其他合同")]
        Other
    }
}
