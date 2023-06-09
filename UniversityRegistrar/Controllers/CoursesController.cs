using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityRegistrar.Controllers;

public class CoursesController : Controller
{
  private readonly UniversityRegistrarContext _db;
  public CoursesController(UniversityRegistrarContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    List<Course> model = _db.Courses
      .Include(course => course.Department)
      .ToList();
    return View(model);
    // return View();
  }
  public ActionResult Create()
  {
    ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "DepartmentName");
    return View();
  }
  [HttpPost]
  public ActionResult Create(Course course)
  {
    _db.Courses.Add(course);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }
}
