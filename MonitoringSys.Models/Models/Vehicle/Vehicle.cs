using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Best Performance : Refrence to last Log Prevent search for last log ...
        /// </summary>
        [ForeignKey("LastVehicleLog")]
        public int? LastVehicleLogId { get; set; }
        public virtual VehicleLog LastVehicleLog { get; set; }

        public virtual ICollection<VehicleLog> VehicleLogs { get; set; }

    }
}
