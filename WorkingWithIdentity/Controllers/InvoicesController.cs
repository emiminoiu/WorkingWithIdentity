using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkingWithIdentity.Data;
using WorkingWithIdentity.Models;

namespace WorkingWithIdentity.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;//injecting current user

        public InvoicesController(ApplicationDbContext _context,
        UserManager<IdentityUser> userManager)
        {
            this._context = _context;
            this.userManager = userManager;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Invoices.Include(i => i.Client);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> ShowClient(string clientId)
        {
            var client = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(clientId));
            return View(client);
        }
        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Client)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Total,Concept,ClientId")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {                 
                    invoice.ClientId = userManager.GetUserId(HttpContext.User);
                    _context.Add(invoice);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                
            }
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", invoice.ClientId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", invoice.ClientId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Total,Concept,ClientId")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", invoice.ClientId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Client)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(string id)
        {
            return _context.Invoices.Any(e => e.Id.Equals(id));
        }
    }
}
