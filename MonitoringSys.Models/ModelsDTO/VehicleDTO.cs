using System;
using System.ComponentModel.DataAnnotations;

namespace MonitoringSys.Models.ModelsDTO
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        [Display(Name = "Last Vehicle Status")]
        public int? LastVehicleLogId { get; set; }

        public DateTime? LastVehicleLogDateTime { get; set; }
        public bool? LastVehicleLogResponse { get; set; }

    }
}