namespace ML.PCM.Permissions;

public static class PCMPermissions
{
    public const string GroupName = "PCM";


    public static class Books
    {
        public const string Default = GroupName + ".Books";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }


    public static class Projects
    {
        public const string Default = GroupName + ".Projects";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
