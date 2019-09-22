using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonitoringSys.DATA;
using MonitoringSys.Models;
using MonitoringSys.Services;

namespace MonitoringSys.Controllers
{
    public class VehicleDTO:Vehicle
    {
        public int FilterCustomerId { get; set; }
        public SelectList Customers { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }

    public class VehiclesController : BaseController<Vehicle>
    {
        private readonly IService<Customer> _CustomerService;
        public VehiclesController(IService<Vehicle> service, IService<Customer> customerService) : base(service)
        {
            _CustomerService = customerService;
        }
        //  GET: Vehicles
        public override async Task<IActionResult> Index(int? id)
        {

            //Use AutoMaper Best for this case in Serivces Layer ...
            VehicleDTO vehicleDTO = new VehicleDTO()
            {
                Customers = new SelectList(await _CustomerService.GetAll(), nameof(Customer.Id), nameof(Customer.Name), -1),
                Vehicles = new List<Vehicle>(await _Service.GetAll(
                                    a => (id == null) ?true :a.CustomerId == id,
                                    a => a.VehicleStatus,a =>a.VehicleStatus.VehicleStatusUpdates)),
                FilterCustomerId = id ?? 0,
            };

            return View(vehicleDTO);
        }

        // GET: Vehicles/Create
        public override IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_CustomerService.GetAll().Result, "Id", "Name");
            return base.Create();
        }

     

    }
}

//        private readonly MainDbContext _context;

//        public VehiclesController(MainDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Vehicles
//        public async Task<IActionResult> Index()
//        {
//            var mainDbContext = _context.Vehicles.Include(v => v.Customer);
//            return View(await mainDbContext.ToListAsync());
//        }

//        // GET: Vehicles/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var vehicle = await _context.Vehicles
//                .Include(v => v.Customer)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (vehicle == null)
//            {
//                return NotFound();
//            }

//            return View(vehicle);
//        }

//        // GET: Vehicles/Create
//        public IActionResult Create()
//        {
//            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
//            return View();
//        }

//        // POST: Vehicles/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Name,Number,CustomerId")] Vehicle vehicle)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(vehicle);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", vehicle.CustomerId);
//            return View(vehicle);
//        }

//        // GET: Vehicles/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var vehicle = await _context.Vehicles.FindAsync(id);
//            if (vehicle == null)
//            {
//                return NotFound();
//            }
//            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", vehicle.CustomerId);
//            return View(vehicle);
//        }

//        // POST: Vehicles/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Number,CustomerId")] Vehicle vehicle)
//        {
//            if (id != vehicle.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(vehicle);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!VehicleExists(vehicle.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", vehicle.CustomerId);
//            return View(vehicle);
//        }

//        // GET: Vehicles/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var vehicle = await _context.Vehicles
//                .Include(v => v.Customer)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (vehicle == null)
//            {
//                return NotFound();
//            }

//            return View(vehicle);
//        }

//        // POST: Vehicles/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var vehicle = await _context.Vehicles.FindAsync(id);
//            _context.Vehicles.Remove(vehicle);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool VehicleExists(int id)
//        {
//            return _context.Vehicles.Any(e => e.Id == id);
//        }
//    }
//}
