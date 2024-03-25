using System.Reflection;

namespace Eva.Infra.Tools.Reflections
{
    public static class Assemblies
    {
        public static List<Type> GetEvaTypes(Type representativeType)
        {
            return representativeType.Assembly.GetTypes().ToList();
        }
    }
}
