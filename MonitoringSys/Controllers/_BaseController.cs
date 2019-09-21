using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonitoringSys.Models;

using MonitoringSys.Services;

namespace MonitoringSys.Controllers
{
    public abstract class BaseController<TEntity> : Controller where TEntity : class, IBaseEntity
    {
        protected readonly IService<TEntity> _Service;
        public BaseController(IService<TEntity> service)
        {
            _Service = service;
        }

        // GET: 
        public virtual async Task<IActionResult> Index()
        {
            return View(await _Service.GetAll());
        }

        // GET: /Details/5
        public virtual async Task<IActionResult> Details(int id)
        {
            if (!IsExists(id))
            {
                return NotFound();
            }

            var entity = await _Service.Get(id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: /Create
        public virtual IActionResult Create()
        {
            return View();
        }

        // POST: /Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                await _Service.Add(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        // GET: Customers/Edit/5
        public virtual async Task<IActionResult> Edit(int id)
        {
            if (!IsExists(id))
            {
                return NotFound();
            }

            var entity = await _Service.Get(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(int id, TEntity entity)
        {
            if (!IsExists(id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (!await _Service.Update(entity))
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        // GET: /Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsExists(id))
            {
                return NotFound();
            }

            var entity = await _Service.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: /Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _Service.Get(id);
            await _Service.Delete(entity);
            return RedirectToAction(nameof(Index));
        }

        private bool IsExists(int id)
        {
            return _Service.Any(id);
        }

    }
}
        //private readonly IService<Customer> _CustomerService;

        //public CustomersController(IService<Customer> service)//,ICustomerRepository customerRepository)
        //{
        //    //_unitOfWork = unitOfWork;
        //    _CustomerService = service;
        //}

        //// GET: Customers
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _CustomerService.GetAll());
        //}

        //// GET: Customers/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var customer = await _CustomerService.Get(id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(customer);
        //}

        //// GET: Customers/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Customers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Mobile,City")] Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _CustomerService.Add(customer);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(customer);
        //}

        //// GET: Customers/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var customer = await _CustomerService.Get(id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(customer);
        //}

        //// POST: Customers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Mobile,City")] Customer customer)
        //{
        //    if (id != customer.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await _CustomerService.Update(customer);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CustomerExists(customer.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(customer);
        //}

        //// GET: Customers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var customer = await _CustomerService.Get(id);

        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(customer);
        //}

        //// POST: Customers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var customer = await _CustomerService.Get(id);
        //    await _CustomerService.Delete(customer);
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CustomerExists(int id)
        //{
        //    return _CustomerService.Get(id).Result is null ? false : true;
        //}

