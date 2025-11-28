using ML.PCM.Localization;
using Volo.Abp.AspNetCore.Components;

namespace ML.PCM;

public abstract class PCMComponentBase : AbpComponentBase
{
    protected PCMComponentBase()
    {
        LocalizationResource = typeof(PCMResource);
    }
}
