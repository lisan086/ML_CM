using Azure.Core;
using ML.PCM.Entities.DecisionDocuments;
using ML.PCM.Entities.Projects;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ML.PCM.Managers.DecisionDocuments;

public class DecisionDocumentManager : DomainService
{
    private readonly IRepository<DecisionDocument, Guid> _repository;

    public DecisionDocumentManager(IRepository<DecisionDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<DecisionDocument> CreateAsync(Guid projectId, string name)
    {
        // 1. 生成项目编号逻辑
        var code = await GenerateNextProjectCodeAsync();
        // 2. 创建实体 (使用 GuidGenerator 生成主键)
        var entity = new DecisionDocument(GuidGenerator.Create(), projectId, code, name);
        return entity;
    }

    private async Task<string> GenerateNextProjectCodeAsync()
    {
        // 编号规则: JC-20250001
        var prefix = $"JC-{DateTime.Now.Year}";

        // ✅ 第一步：获取 Queryable 对象（这是 EF Core 查询的入口）
        var queryable = await _repository.GetQueryableAsync();

        // ✅ 第二步：使用 AsyncExecuter 执行 LINQ 查询
        // 逻辑：先筛选(Where) -> 再倒序(OrderByDescending) -> 取第一个(FirstOrDefault)
        var lastProject = await AsyncExecuter.FirstOrDefaultAsync(
            queryable.Where(p => p.DocumentCode.StartsWith(prefix))
                .OrderByDescending(p => p.DocumentCode)
        );

        int nextNumber = 1;
        if (lastProject != null)
        {
            // 解析最后几位数字
            var lastCode = lastProject.DocumentCode;
            var numberPart = lastCode.Replace(prefix, "");
            if (int.TryParse(numberPart, out int currentNumber))
            {
                nextNumber = currentNumber + 1;
            }
        }

        return $"{prefix}{nextNumber:0000}"; // 例如 JC-20250001
    }
}

