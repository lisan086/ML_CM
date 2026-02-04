using System;
using System.Linq;
using System.Threading.Tasks;
using ML.PCM.Entities.Projects;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ML.PCM.Managers.Projects;

public class ProjectManager : DomainService
{
    private readonly IRepository<Project, Guid> _projectRepository;

    public ProjectManager(IRepository<Project, Guid> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Project> CreateAsync(string name)
    {
        // 1. 生成项目编号逻辑
        var code = await GenerateNextProjectCodeAsync();

        // 2. 创建实体 (使用 GuidGenerator 生成主键)
        var project = new Project(GuidGenerator.Create(), code, name);

        return project;
    }

    private async Task<string> GenerateNextProjectCodeAsync()
    {
        // 逻辑：P-20250001 (P-年份+4位序号)
        var yearPrefix = $"P-{DateTime.Now.Year}";

        // ✅ 第一步：获取 Queryable 对象（这是 EF Core 查询的入口）
        var queryable = await _projectRepository.GetQueryableAsync();

        // ✅ 第二步：使用 AsyncExecuter 执行 LINQ 查询
        // 逻辑：先筛选(Where) -> 再倒序(OrderByDescending) -> 取第一个(FirstOrDefault)
        var lastProject = await AsyncExecuter.FirstOrDefaultAsync(
            queryable.Where(p => p.ProjectCode.StartsWith(yearPrefix))
                .OrderByDescending(p => p.ProjectCode)
        );

        int nextNumber = 1;
        if (lastProject != null)
        {
            // 解析最后几位数字
            var lastCode = lastProject.ProjectCode;
            var numberPart = lastCode.Replace(yearPrefix, "");
            if (int.TryParse(numberPart, out int currentNumber))
            {
                nextNumber = currentNumber + 1;
            }
        }

        return $"{yearPrefix}{nextNumber:0000}"; // 例如 P-20250001
    }
}