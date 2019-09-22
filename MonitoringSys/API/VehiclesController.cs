using Microsoft.AspNetCore.Mvc;
using MonitoringSys.Models;
using MonitoringSys.Services;
using System.Threading.Tasks;

namespace MonitoringSys.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : BaseAPIController<Vehicle>
    {
      private  readonly IVehicleService _VehicleService;
        public VehiclesController(IService<Vehicle> service) : base(service)
        {
            _VehicleService = service as IVehicleService;
        }

        /// <summary>
        ///  Update Vehicle Status add New Log if Status Changed ...
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IsResponse"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVehicleStatus")]
        public async Task<bool> UpdateVehicleStatusAsync([FromBody] int id, bool IsResponse)
        {
            return await _VehicleService.UpdateVehicleStatus(id, IsResponse);
        }
    }
}