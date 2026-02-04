using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.PCM.Enums
{
    public enum ProjectCategory
    {
        [Description("省重点")]
        Provincial,

        [Description("市重点")]
        Municipal,

        [Description("县重点")]
        County,

        [Description("重点民生工程")]
        KeyLivelihood,

        [Description("一般项目")]
        General
    }
}
