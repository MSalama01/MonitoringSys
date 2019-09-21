using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringSys.Models
{
    public class VehicleStatus : IBaseEntity<int>
    {

        [ForeignKey("Vehicle")]
        public int Id { get; set; }
        public virtual Vehicle Vehicle { get; set; }


        public int? LastVehicleStatusUpdateId { get; set; }
        //[ForeignKey("LastVehicleStatusUpdateId")]
        //public virtual VehicleStatusUpdate LastVehicleStatusUpdate { get; set; }

        public virtual List<VehicleStatusUpdate> VehicleStatusUpdates { get; set; }


    }
}
