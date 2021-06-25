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
using StudentRegisterProject.Services.Services.Nationalities;
using StudentRegisterProject.Services.Services.Students;

namespace StudentRegisterProject.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IStudentService _service;
        private readonly IFacultyService _facultyService;
        private readonly INationalityService _nationalityService;

        public StudentsController(IStudentService service, IFacultyService facultyService, INationalityService nationalityService)
        {
            _service = service;
            _facultyService = facultyService;
            _nationalityService = nationalityService;
        }

        // GET: Students
        public async Task<IActionResult> Index(string SearchString)
        {
            var students = await _service.GetAllStudents(SearchString);
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _service.GetStudent((int)id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public async Task<IActionResult> Create()
        {
            var faculties = await _facultyService.GetAllFaculties();
            var nationalities = await _nationalityService.GetAllNationalities();
            ViewData["FacultyId"] = new SelectList(faculties, "Id", "Address");
            ViewData["NationalityId"] = new SelectList(nationalities, "Id", "Title");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedBy,UpdatedBy,CreatedOn,UpdatedOn,FirstName,LastName,FacultyNumber,NationalityId,FacultyId")] StudentDTO student)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            var faculties = await _facultyService.GetAllFaculties();
            var nationalities = await _nationalityService.GetAllNationalities();
            ViewData["FacultyId"] = new SelectList(faculties, "Id", "Address", student.FacultyId);
            ViewData["NationalityId"] = new SelectList(nationalities, "Id", "Title", student.NationalityId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _service.GetStudent((int)id);
            if (student == null)
            {
                return NotFound();
            }
            var faculties = await _facultyService.GetAllFaculties();
            var nationalities = await _nationalityService.GetAllNationalities();
            ViewData["FacultyId"] = new SelectList(faculties, "Id", "Address", student.FacultyId);
            ViewData["NationalityId"] = new SelectList(nationalities, "Id", "Title", student.NationalityId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedBy,UpdatedBy,CreatedOn,UpdatedOn,FirstName,LastName,FacultyNumber,NationalityId,FacultyId")] StudentDTO student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateStudent(student, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await StudentExists(student.Id)))
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
            var faculties = await _facultyService.GetAllFaculties();
            var nationalities = await _nationalityService.GetAllNationalities();
            ViewData["FacultyId"] = new SelectList(faculties, "Id", "Address", student.FacultyId);
            ViewData["NationalityId"] = new SelectList(nationalities, "Id", "Title", student.NationalityId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _service.GetStudent((int)id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteStudent(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> StudentExists(int id)
        {
            var student = await _service.GetStudent(id);
            return student == null ? false : true;
        }
    }
}
