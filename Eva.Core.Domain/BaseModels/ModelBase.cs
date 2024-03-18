using System.ComponentModel.DataAnnotations;

namespace Eva.Core.Domain.BaseModels
{
    public class ModelBase : Trackable
    {
        public int Id { get; set; }
    }
}
