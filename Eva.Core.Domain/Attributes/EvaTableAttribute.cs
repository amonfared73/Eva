using System.ComponentModel.DataAnnotations.Schema;

namespace Eva.Core.Domain.Attributes
{
    public class EvaTableAttribute : TableAttribute
    {
        public EvaTableAttribute(string name) : base(name)
        {
        }
    }
}
