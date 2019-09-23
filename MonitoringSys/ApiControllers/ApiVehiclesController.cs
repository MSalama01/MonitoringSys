using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoringSys.DATA;
using MonitoringSys.Models;
using MonitoringSys.Services;

namespace MonitoringSys.ApiControllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ApiVehiclesController : ApiBaseController<Vehicle>
    {
        private readonly IVehicleService _VehicleService;
        public ApiVehiclesController(IVehicleService service) : base(service)
        {
            _VehicleService = service;
        }


        [HttpPost, Route("UpdateVehicleStatus")]
        public async Task<bool> UpdateVehicleStatus([FromBody] int id, bool IsResponse)
        {
            return await _VehicleService.UpdateVehicleStatus(id, IsResponse);
        }

    }
}

//        private readonly _context;

//        public VehiclesController(MainDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Vehicles
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
//        {
//            return await _context.Vehicles.ToListAsync();
//        }

//        // GET: api/Vehicles/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
//        {
//            var vehicle = await _context.Vehicles.FindAsync(id);

//            if (vehicle == null)
//            {
//                return NotFound();
//            }

//            return vehicle;
//        }

//        // PUT: api/Vehicles/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//        // more details see https://aka.ms/RazorPagesCRUD.
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
//        {
//            if (id != vehicle.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(vehicle).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!VehicleExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Vehicles
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//        // more details see https://aka.ms/RazorPagesCRUD.
//        [HttpPost]
//        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
//        {
//            _context.Vehicles.Add(vehicle);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
//        }

//        // DELETE: api/Vehicles/5
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<Vehicle>> DeleteVehicle(int id)
//        {
//            var vehicle = await _context.Vehicles.FindAsync(id);
//            if (vehicle == null)
//            {
//                return NotFound();
//            }

//            _context.Vehicles.Remove(vehicle);
//            await _context.SaveChangesAsync();

//            return vehicle;
//        }

//        private bool VehicleExists(int id)
//        {
//            return _context.Vehicles.Any(e => e.Id == id);
//        }
//    }
//}
