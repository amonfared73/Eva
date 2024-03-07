using System.ComponentModel.DataAnnotations;

namespace Eva.Core.Domain.BaseModels
{
    public class DomainObject : Trackable
    {
        public int Id { get; set; }
    }
}
