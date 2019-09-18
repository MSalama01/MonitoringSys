using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringSys.Models
{
    public class VehicleStatusUpdate : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool IsResponse { get; set; }

        public int VehicleStatusId { get; set; }
        [ForeignKey("VehicleStatusId")]
        public virtual VehicleStatus VehicleStatus { get; set; }
    }
}
