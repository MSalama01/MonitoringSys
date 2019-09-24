using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringSys.Models
{
    public class VehicleLog : IBaseEntity<int>
    {
        
        public int Id { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool IsResponse { get; set; }


        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
