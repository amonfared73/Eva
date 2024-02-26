namespace Eva.Core.Domain.BaseModels
{
    public class EvaProject : Enumeration
    {
        public static readonly EvaProject Domain = new EvaProject(1, "Eva.Core.Domain");
        public static readonly EvaProject ApplicationService = new EvaProject(2, "Eva.Core.ApplicationService");
        public static readonly EvaProject EntityFramework = new EvaProject(3, "Eva.Infra.EntityFramework");
        public static readonly EvaProject Tools = new EvaProject(4, "Eva.Infra.Tools");
        public static readonly EvaProject API = new EvaProject(5, "Eva.EndPoint.API");
        public static readonly EvaProject xUnit = new EvaProject(6, "Eva.UnitTest.xUnit");
        private EvaProject()
        {

        }
        private EvaProject(int id, string value) : base(id, value)
        {

        }
    }
}
