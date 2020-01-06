using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApplication.Data;
using SampleApplication.Models.Entities;
using SampleApplication.Services;

namespace SampleApplication.Controllers
{
    public class CustomersController : Controller
    {
        private readonly DataContext _context;
        private readonly Payment _payment;

        public CustomersController(
            Payment payment,
            DataContext context)
        {
            _payment = payment;
            _context = context;
        }


        // GET: Customers
        public async Task<IActionResult> Index()
        {
            if (_context.Customers.Count() == 0)
                CreateSampleCustomer();

            var customers = await
                _context.Customers
                    .Include(c => c.Cards)
                    .Include(c => c.Accounts)
                    .AsNoTracking()
                    .ToListAsync();

            return View(customers);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create() => View();

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,ShippingAddress")] Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            customer.Id = Guid.NewGuid();
            customer.TrusttId = _payment.CreateCustomer(customer);
            _context.Add(customer);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Email,ShippingAddress")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(Guid id) => _context.Customers.Any(e => e.Id == id);


        private void CreateSampleCustomer()
        {
            _context.Customers.Add(
                new Customer
                {
                    Email = "somebody@domain.com",
                    Name = "Some Body",
                    TrusttId = Guid.NewGuid(),
                    Cards = new List<Card>
                    {
                        new Card
                        {
                            CVV = "123",
                            Number = "969989247298347298",
                            Month = "1",
                            Year = "2021"
                        }
                    },
                    Accounts = new List<Account>
                    {
                        new Account
                        {
                            IBAN = "EKHSJDDAAD",
                            Swift = "12313123123",
                            Name = "SOME BODY"
                        }
                    }
                });

            _context.SaveChanges();

        } // private

    } // class
}
