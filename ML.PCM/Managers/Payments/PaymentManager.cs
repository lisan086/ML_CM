using Microsoft.CodeAnalysis;
using ML.PCM.Entities.Payments;
using ML.PCM.Entities.Permits;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ML.PCM.Managers.Payments;

public class PaymentManager : DomainService
{
    private readonly IRepository<Payment, Guid> _repository;

    public PaymentManager(IRepository<Payment, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<Payment> CreateAsync(Guid contractId)
    {
        // 1. 生成项目编号逻辑
        var code = await GenerateNextProjectCodeAsync();
        // 2. 创建实体 (使用 GuidGenerator 生成主键)
        var entity = new Payment(GuidGenerator.Create(), contractId, code);
        return entity;
    }

    private async Task<string> GenerateNextProjectCodeAsync()
    {
        // 编号规则: XK-yyyy-0001
        var prefix = $"ZF-{DateTime.Now.Year}";

        // 简单生成逻辑
        var count = await _repository.CountAsync(x => x.DocumentCode.StartsWith(prefix));
        var code = $"{prefix}-{(count + 1):D4}";

        return code; // 例如 XK-yyyy-0001
    }
}
