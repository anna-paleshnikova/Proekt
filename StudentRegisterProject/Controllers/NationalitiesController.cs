using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentRegisterProject.Data.Context;
using StudentRegisterProject.Data.Entities;
using StudentRegisterProject.Services.DTO;
using StudentRegisterProject.Services.Services.Nationalities;

namespace StudentRegisterProject.Controllers
{
    [Authorize]
    public class NationalitiesController : Controller
    {
        private readonly INationalityService _service;
        private readonly UserManager<User> _userManager;

        public NationalitiesController(INationalityService service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: Nationalities
        public async Task<IActionResult> Index(string SearchString)
        {
            var nationalities = await _service.GetAllNationalities(SearchString);
            return View(nationalities);
        }

        // GET: Nationalities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationality = await _service.GetNationality((int)id);
            if (nationality == null)
            {
                return NotFound();
            }

            return View(nationality);
        }

        // GET: Nationalities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nationalities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedBy,UpdatedBy,CreatedOn,UpdatedOn,Title")] NationalityDTO nationality)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateNationality(nationality);
                return RedirectToAction(nameof(Index));
            }
            return View(nationality);
        }

        // GET: Nationalities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationality = await _service.GetNationality((int)id);
            if (nationality == null)
            {
                return NotFound();
            }
            return View(nationality);
        }

        // POST: Nationalities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedBy,UpdatedBy,CreatedOn,UpdatedOn,Title")] NationalityDTO nationality)
        {
            if (id != nationality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateNationality(nationality, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await NationalityExists(nationality.Id)))
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
            return View(nationality);
        }

        // GET: Nationalities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationality = await _service.GetNationality((int)id);
            if (nationality == null)
            {
                return NotFound();
            }

            return View(nationality);
        }

        // POST: Nationalities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteNationality(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> NationalityExists(int id)
        {
            var nationality = await _service.GetNationality(id);
            return nationality == null ? false : true;
        }
    }
}
