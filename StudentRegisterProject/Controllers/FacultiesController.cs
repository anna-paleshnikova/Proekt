using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentRegisterProject.Data.Context;
using StudentRegisterProject.Data.Entities;
using StudentRegisterProject.Services.DTO;
using StudentRegisterProject.Services.Services.Faculties;

namespace StudentRegisterProject.Controllers
{
    [Authorize]
    public class FacultiesController : Controller
    {
        private readonly IFacultyService _service;

        public FacultiesController(IFacultyService service)
        {
            _service = service;
        }

        // GET: Faculties
        public async Task<IActionResult> Index(string SearchString)
        {
            var faculties = await _service.GetAllFaculties(SearchString);
            return View(faculties);
        }

        // GET: Faculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _service.GetFaculty((int)id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // GET: Faculties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedBy,UpdatedBy,CreatedOn,UpdatedOn,Name,City,Address")] FacultyDTO faculty)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateFaculty(faculty);
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _service.GetFaculty((int)id);
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedBy,UpdatedBy,CreatedOn,UpdatedOn,Name,City,Address")] FacultyDTO faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedFaculty = await _service.UpdateFaculty(faculty, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await FacultyExists(faculty.Id)))
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
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _service.GetFaculty((int)id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteFaculty(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FacultyExists(int id)
        {
            var faculty = await _service.GetFaculty(id);
            return faculty == null ? false : true ;
        }
    }
}
