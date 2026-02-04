using ML.PCM.Entities.Permits;
using ML.PCM.Entities.Procurements;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ML.PCM.Managers.Procurements;

public class ProcurementManager : DomainService
{
    private readonly IRepository<Procurement, Guid> _repository;

    public ProcurementManager(IRepository<Procurement, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<Procurement> CreateAsync(Guid projectId)
    {
        // 1. 生成项目编号逻辑
        var code = await GenerateNextProjectCodeAsync();
        // 2. 创建实体 (使用 GuidGenerator 生成主键)
        var entity = new Procurement(GuidGenerator.Create(), projectId, code);
        return entity;
    }

    private async Task<string> GenerateNextProjectCodeAsync()
    {
        // 编号规则: ZC-yyyy-0001
        var prefix = $"ZC-{DateTime.Now.Year}";

        // 简单生成逻辑
        var count = await _repository.CountAsync(x => x.DocumentCode.StartsWith(prefix));
        var code = $"{prefix}-{(count + 1):D4}";

        return code; // 例如 ZC-yyyy-0001
    }
}