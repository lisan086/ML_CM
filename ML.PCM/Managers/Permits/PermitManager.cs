using ML.PCM.Entities.DecisionDocuments;
using ML.PCM.Entities.Permits;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ML.PCM.Managers.Permits;

public class PermitManager : DomainService
{
    private readonly IRepository<Permit, Guid> _repository;

    public PermitManager(IRepository<Permit, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<Permit> CreateAsync(Guid projectId, string name)
    {
        // 1. 生成项目编号逻辑
        var code = await GenerateNextProjectCodeAsync();
        // 2. 创建实体 (使用 GuidGenerator 生成主键)
        var entity = new Permit(GuidGenerator.Create(), projectId, code, name);
        return entity;
    }

    private async Task<string> GenerateNextProjectCodeAsync()
    {
        // 编号规则: XK-yyyy-0001
        var prefix = $"XK-{DateTime.Now.Year}";

        // 简单生成逻辑
        var count = await _repository.CountAsync(x => x.DocumentCode.StartsWith(prefix));
        var code = $"{prefix}-{(count + 1):D4}";

        return code; // 例如 JC-20250001
    }
}