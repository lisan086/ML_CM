using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.PCM.Enums
{
    public enum ProcurementCategory
    {
        [Description("公开招标")]
        GZ, // 公招

        [Description("内部招采")]
        NZ, // 内招

        [Description("询价")]
        XJ, // 询价

        [Description("其他")]
        QT  // 其他
    }
}
