using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringSys.Models.ModelsDTO
{
    public class VehicleListDTO : VehicleDTO
    {
        public int? FilterCustomerId { get; set; }
        public SelectList FilterCustomers { get; set; }
        public List<VehicleDTO> Vehicles { get; set; }
    }
}
