using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringSys.Models
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
