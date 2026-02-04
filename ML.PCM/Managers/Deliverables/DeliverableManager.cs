using Microsoft.CodeAnalysis;
using ML.PCM.Entities.Deliverables;
using ML.PCM.Entities.Procurements;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ML.PCM.Managers.Deliverables;

public class DeliverableManager : DomainService
{
    private readonly IRepository<Deliverable, Guid> _repository;

    public DeliverableManager(IRepository<Deliverable, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<Deliverable> CreateAsync(Guid contractId, string name)
    {
        // 1. 生成项目编号逻辑
        var code = await GenerateNextProjectCodeAsync();
        // 2. 创建实体 (使用 GuidGenerator 生成主键)
        var entity = new Deliverable(GuidGenerator.Create(), contractId, code, name);
        return entity;
    }

    private async Task<string> GenerateNextProjectCodeAsync()
    {
        // 编号规则: ZC-yyyy-0001
        var prefix = $"CG-{DateTime.Now.Year}";

        // 简单生成逻辑
        var count = await _repository.CountAsync(x => x.DocumentCode.StartsWith(prefix));
        var code = $"{prefix}-{(count + 1):D4}";

        return code; // 例如 ZC-yyyy-0001
    }
}
