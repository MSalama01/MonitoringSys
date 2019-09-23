using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MonitoringSys.Models
{
    public class VehicleStatus : IBaseEntity<int>
    {

        [ForeignKey("Vehicle")]
        public int Id { get; set; }
        public virtual Vehicle Vehicle { get; set; }


        public int? LastVehicleStatusLogId { get; set; }

        [NotMapped]
        [ForeignKey("LastVehicleStatusLogId")]
        public virtual VehicleStatusLog LastVehicleStatusLog { get; set; }

        public virtual List<VehicleStatusLog> VehicleStatusLogs { get; set; }

        //[NotMapped]
        //public virtual VehicleStatusLog LastVehicleStatusLog
        //{
        //    get
        //    {
        //        return LastVehicleStatusLog == null ? null :
        //            VehicleStatusLogs.FirstOrDefault(a => a.Id==LastVehicleStatusLogId);
        //    }

        //}


    }
}
