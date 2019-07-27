using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechProject.Models;

namespace TechProject.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ContactPersonsController : Controller
    {
        private readonly TekcropContext _context;

        public ContactPersonsController(TekcropContext context)
        {
            _context = context;
        }

        // GET: ContactPersons
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactPerson.ToListAsync());
        }

        // GET: ContactPersons/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPerson = await _context.ContactPerson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactPerson == null)
            {
                return NotFound();
            }

            return View(contactPerson);
        }
        // GET: ContactPersons/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPerson = await _context.ContactPerson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactPerson == null)
            {
                return NotFound();
            }

            return View(contactPerson);
        }

        // POST: ContactPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var contactPerson = await _context.ContactPerson.FindAsync(id);
            _context.ContactPerson.Remove(contactPerson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactPersonExists(long id)
        {
            return _context.ContactPerson.Any(e => e.Id == id);
        }
    }
}
