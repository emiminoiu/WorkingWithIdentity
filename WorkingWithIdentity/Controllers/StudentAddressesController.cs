using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkingWithIdentity.Data;
using WorkingWithIdentity.Models;
using WorkingWithIdentity.ViewModels;

namespace WorkingWithIdentity.Controllers
{
    public class StudentAddressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentAddressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentAddresses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentAddress.Include(s => s.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentAddresses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAddress = await _context.StudentAddress
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAddress == null)
            {
                return NotFound();
            }

            return View(studentAddress);
        }

        // GET: StudentAddresses/Create
        public IActionResult Create()
        {
            List<string> names = new List<string>();
            var students = _context.Students.ToList();
            foreach(var student in students)
            {
                names.Add(student.Name);
            }
           

            ViewData["StudentName"] = new SelectList(names);
            return View();
        }

        // POST: StudentAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Address,StudentName")] StudentAddressViewModel studentAddress)
        {
            StudentAddress theStudentAddress = new StudentAddress();
            if (ModelState.IsValid)
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Name.Equals(studentAddress.StudentName));
                theStudentAddress.StudentId = student.Id;
                theStudentAddress.Address = (studentAddress.Address);
                _context.Add(theStudentAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", studentAddress.StudentId);
            return View(theStudentAddress);
        }

        // GET: StudentAddresses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAddress = await _context.StudentAddress.FindAsync(id);
            if (studentAddress == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", studentAddress.StudentId);
            return View(studentAddress);
        }

        // POST: StudentAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Address,StudentId,Id")] StudentAddress studentAddress)
        {
            if (id != studentAddress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentAddressExists(studentAddress.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", studentAddress.StudentId);
            return View(studentAddress);
        }

        // GET: StudentAddresses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAddress = await _context.StudentAddress
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAddress == null)
            {
                return NotFound();
            }

            return View(studentAddress);
        }

        // POST: StudentAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var studentAddress = await _context.StudentAddress.FindAsync(id);
            _context.StudentAddress.Remove(studentAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentAddressExists(string id)
        {
            return _context.StudentAddress.Any(e => e.Id == id);
        }
    }
}
