using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.PCM.Enums
{
    public enum PermitCategory
    {
        [Description("用地许可")]
        YD, // 用地

        [Description("环评许可")]
        HP, // 环评

        [Description("建设工程规划许可")]
        JS, // 建设

        [Description("施工许可")]
        SG, // 施工

        [Description("其他许可")]
        QT  // 其他
    }
}
