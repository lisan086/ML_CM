using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.PCM.Enums
{
    public enum PaymentCategory
    {
        [Description("工程费")]
        EngineeringFee,

        [Description("服务费")]
        ServiceFee,

        [Description("其他费")]
        OtherFee
    }
}
