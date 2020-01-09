using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SampleApplication.Data;
using SampleApplication.Models.Entities;
using SampleApplication.Services;

namespace SampleApplication.Controllers
{
    public class ChargesController : Controller
    {
        private readonly DataContext _context;
        private readonly Payment _trustt;

        public ChargesController(DataContext context, Payment payment)
        {
            _trustt = payment;
            _context = context;
        }

        // GET: Charges
        public async Task<IActionResult> Index(Guid id)
        {
            ViewData["CustomerId"] = id.ToString();
            var dataContext = _context.Charge.Include(c => c.Customer).Where(c => c.CustomerId == id);
            return View(await dataContext.ToListAsync());
        }

        // GET: Charges/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charge = await _context.Charge
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (charge == null)
            {
                return NotFound();
            }

            return View(charge);
        }

        // GET: Charges/Create
        public IActionResult Create(Guid id)
        {
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
            ViewData["CustomerId"] = id;
            return View();
        }

        // POST: Charges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Currency,Amount,ChargeType,CustomerId")] Charge charge)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", new RouteValueDictionary(new { Id = charge.CustomerId }));

            charge.BeginTime = DateTime.Now;
            charge.EndTime = DateTime.Now;
            charge.ChargeStatus = ChargeStatus.Complete;
            charge = _trustt.Charge(charge);
            _context.Add(charge);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new RouteValueDictionary(new { Id = charge.CustomerId }));
        }


        // POST: Charges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EndTime,BeginTime,Currency,Amount,ChargeType,CustomerId,Id")] Charge charge)
        {
            if (id != charge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(charge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChargeExists(charge.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", charge.CustomerId);
            return View(charge);
        }


        // GET: Charges/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charge = await _context.Charge
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (charge == null)
            {
                return NotFound();
            }

            return View(charge);
        }

        // POST: Charges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var charge = await _context.Charge.FindAsync(id);
            _context.Charge.Remove(charge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChargeExists(Guid id) => _context.Charge.Any(e => e.Id == id);
    }
}
