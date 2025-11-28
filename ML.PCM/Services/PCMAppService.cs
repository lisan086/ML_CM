using Volo.Abp.Application.Services;
using ML.PCM.Localization;

namespace ML.PCM.Services;

/* Inherit your application services from this class. */
public abstract class PCMAppService : ApplicationService
{
    protected PCMAppService()
    {
        LocalizationResource = typeof(PCMResource);
    }
}