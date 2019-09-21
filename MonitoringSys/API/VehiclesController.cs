using Microsoft.AspNetCore.Mvc;
using MonitoringSys.Models;
using MonitoringSys.Services;

namespace MonitoringSys.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : BaseAPIController<Vehicle>
    {
        public VehiclesController(IService<Vehicle> service) : base(service)
        {
        }
    }
}