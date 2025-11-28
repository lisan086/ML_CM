using Microsoft.Extensions.Localization;
using ML.PCM.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ML.PCM;

[Dependency(ReplaceServices = true)]
public class PCMBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<PCMResource> _localizer;

    public PCMBrandingProvider(IStringLocalizer<PCMResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
