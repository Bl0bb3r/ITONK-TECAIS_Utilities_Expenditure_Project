using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Accountancy.Data;
using Accountancy.Models;

namespace Accountancy.Controllers
{
    public class HouseholdModelsController : Controller
    {
        private readonly AccountancyContext _context;

        public HouseholdModelsController(AccountancyContext context)
        {
            _context = context;
        }

        // GET: HouseholdModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Households.ToListAsync());
        }

        // GET: HouseholdModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdModel = await _context.Households
                .FirstOrDefaultAsync(m => m.ID == id);
            if (householdModel == null)
            {
                return NotFound();
            }

            return View(householdModel);
        }

        // GET: HouseholdModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HouseholdModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID")] HouseholdModel householdModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(householdModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(householdModel);
        }

        // GET: HouseholdModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdModel = await _context.Households.FindAsync(id);
            if (householdModel == null)
            {
                return NotFound();
            }
            return View(householdModel);
        }

        // POST: HouseholdModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] HouseholdModel householdModel)
        {
            if (id != householdModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(householdModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseholdModelExists(householdModel.ID))
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
            return View(householdModel);
        }

        // GET: HouseholdModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdModel = await _context.Households
                .FirstOrDefaultAsync(m => m.ID == id);
            if (householdModel == null)
            {
                return NotFound();
            }

            return View(householdModel);
        }

        // POST: HouseholdModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var householdModel = await _context.Households.FindAsync(id);
            _context.Households.Remove(householdModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseholdModelExists(int id)
        {
            return _context.Households.Any(e => e.ID == id);
        }
    }
}
