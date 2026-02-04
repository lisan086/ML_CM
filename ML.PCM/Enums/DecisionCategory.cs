using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.PCM.Enums
{
    public enum DecisionCategory
    {
        [Description("政府类")]
        ZF, // 政府

        [Description("公司类")]
        GS, // 公司

        [Description("财经")]
        CJ,

        [Description("常务")]
        CW,

        [Description("常委")]
        CWe, // 避免与 CW (常务) 冲突

        [Description("党组")]
        DZ,

        [Description("总办")]
        ZB,

        [Description("董办")]
        DB,

        [Description("签批")]
        QP,

        [Description("其他类")]
        QT  // 其他
    }
}
