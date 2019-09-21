using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringSys.Models
{
    public class Vehicle : IBaseEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Number { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual VehicleStatus VehicleStatus { get; set; }
    }
}
