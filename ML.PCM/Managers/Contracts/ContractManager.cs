using ML.PCM.Entities.Contracts;
using ML.PCM.Entities.Permits;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ML.PCM.Managers.Contracts;

public class ContractManager : DomainService
{
    private readonly IRepository<Contract, Guid> _repository;

    public ContractManager(IRepository<Contract, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<Contract> CreateAsync(Guid projectId, string name)
    {
        // 1. 生成项目编号逻辑
        var code = await GenerateNextProjectCodeAsync();
        // 2. 创建实体 (使用 GuidGenerator 生成主键)
        var entity = new Contract(GuidGenerator.Create(), projectId, code, name);
        return entity;
    }

    private async Task<string> GenerateNextProjectCodeAsync()
    {
        // 编号规则: HT-yyyy-0001
        var prefix = $"HT-{DateTime.Now.Year}";

        // 简单生成逻辑
        var count = await _repository.CountAsync(x => x.DocumentCode.StartsWith(prefix));
        var code = $"{prefix}-{(count + 1):D4}";

        return code; // 例如 HT-yyyy-0001
    }
}