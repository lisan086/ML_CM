using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.PCM.Enums
{
    public enum DeliverableCategory
    {
        [Description("图纸")]
        Drawing,

        [Description("文本文档")]
        TextDocument,

        [Description("清单")]
        ListOfItems,

        [Description("其他")]
        Other
    }
}
