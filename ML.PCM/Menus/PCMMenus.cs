namespace ML.PCM.Menus;

public class PCMMenus
{
    private const string Prefix = "PCM";
    public const string Home = Prefix + ".Home";


    // 工程管理 (父级菜单)
    public const string EngineeringManagement = Prefix + ".Engineering";

    // 子菜单项
    public const string Projects = Prefix + ".Projects";
    public const string Contracts = Prefix + ".Contracts";
    public const string Procurements = Prefix + ".Procurements";
    public const string Payments = Prefix + ".Payments";
    public const string DecisionDocuments = Prefix + ".DecisionDocuments";
    public const string Permits = Prefix + ".Permits";
    public const string Deliverables = Prefix + ".Deliverables";
}
