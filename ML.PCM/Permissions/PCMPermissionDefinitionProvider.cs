using ML.PCM.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace ML.PCM.Permissions;

public class PCMPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PCMPermissions.GroupName);


        var booksPermission = myGroup.AddPermission(PCMPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(PCMPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(PCMPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(PCMPermissions.Books.Delete, L("Permission:Books.Delete"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(PCMPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PCMResource>(name);
    }
}
