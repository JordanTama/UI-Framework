using Managers;

namespace UI.Core
{
    public static class ArchitectureProxy
    {
        public static T GetManager<T>() where T : Manager
        {
            return M.GetOrNull<T>();
        }
    }
}