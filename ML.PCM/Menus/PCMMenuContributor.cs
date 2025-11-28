using ML.PCM.Localization;
using ML.PCM.Menus;
using ML.PCM.Permissions;
using MudBlazor;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace ML.PCM.Menus;

public class PCMMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var L = context.GetLocalizer<PCMResource>();

        //首页
        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                PCMMenus.Home,
                L["Menu:Home"],
                "/",
                icon: Icons.Material.Filled.Dashboard,
                order: 0
            )
        );

        // 2. 工程管理 (父菜单)
        // icon: "fas fa-industry" 或者 "fas fa-building"
        var engineeringMenu = new ApplicationMenuItem(
            PCMMenus.EngineeringManagement,
            L["Menu:EngineeringManagement"],
            icon: Icons.Material.Filled.Construction,
            order: 1
        );

        // 2.1 项目管理
        engineeringMenu.AddItem(new ApplicationMenuItem(
            PCMMenus.Projects,
            L["Menu:Projects"],
            url: "/projects_main",
            icon: Icons.Material.Filled.AccountTree
        // requiredPermissionName: PCMPermissions.Projects.Default // 暂时注释权限，确保先能看到菜单
        ));

        // 2.2 合同管理
        engineeringMenu.AddItem(new ApplicationMenuItem(
            PCMMenus.Contracts,
            L["Menu:Contracts"],
            url: "/projects/contracts",
            icon: Icons.Material.Filled.Gavel
        ));

        // 2.3 招采信息
        engineeringMenu.AddItem(new ApplicationMenuItem(
            PCMMenus.Procurements,
            L["Menu:Procurements"],
            url: "/projects/procurements",
            icon: Icons.Material.Filled.ShoppingCart
        ));

        // 2.4 资金支付
        engineeringMenu.AddItem(new ApplicationMenuItem(
            PCMMenus.Payments,
            L["Menu:Payments"],
            url: "/projects/payments",
            icon: Icons.Material.Filled.AttachMoney
        ));

        // 2.5 决策文件
        engineeringMenu.AddItem(new ApplicationMenuItem(
            PCMMenus.DecisionDocuments,
            L["Menu:DecisionDocuments"],
            url: "/projects/decision-documents",
            icon: Icons.Material.Filled.Description
        ));

        // 2.6 行政许可
        engineeringMenu.AddItem(new ApplicationMenuItem(
            PCMMenus.Permits,
            L["Menu:Permits"],
            url: "/projects/permits",
            icon: Icons.Material.Filled.AssignmentTurnedIn
        ));

        // 2.7 成果文件
        engineeringMenu.AddItem(new ApplicationMenuItem(
            PCMMenus.Deliverables,
            L["Menu:Deliverables"],
            url: "/projects/deliverables",
            icon: Icons.Material.Filled.Topic
        ));

        // 将构建好的工程管理菜单加入主菜单
        context.Menu.AddItem(engineeringMenu);

        //处理 ABP 自带的管理菜单 (Administration)
        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 999;
        administration.Icon = Icons.Material.Filled.AdminPanelSettings; // 替换掉默认的 fa-wrench


        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        //Administration->Tenant Management
        administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 2);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 7);

        return Task.CompletedTask;
    }
}
