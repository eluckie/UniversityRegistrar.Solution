using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using System.Linq;
namespace UniversityRegistrar.Controllers;

public class StudentsController : Controller
{
  private readonly UniversityRegistrarContext _db;

  public StudentsController(UniversityRegistrarContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    List<Student> model = _db.Students
      .Include(student=>student.Major)
      .Include(student=>student.Enrollments)
      .ThenInclude(join=>join.Course)
      .ToList();
    return View(model);
  }

  public ActionResult Create()
  {
    ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "DepartmentName");
    return View();
  }

  [HttpPost]
  public ActionResult Create(Student newStudent)
  {
    _db.Students.Add(newStudent);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Details(int id)
  {
    Student targetStudent = _db.Students
      .Include(thing => thing.Major)
      .Include(thingy=>thingy.Enrollments)
      .ThenInclude(jointhingy=>jointhingy.Course)
      .ThenInclude(join => join.Department)
      .FirstOrDefault(peep => peep.StudentId == id);
    return View(targetStudent);
  }

  public ActionResult AddCourses(int id)
  {
    Student targetStudent = _db.Students.FirstOrDefault(item => item.StudentId == id);
    ViewBag.Courses = _db.Courses
      .Include(thingy => thingy.Department)
      .ToList();
    return View(targetStudent);
  }

  [HttpPost]
  public ActionResult AddCourses(List<int> wutCourses, int studentId)
  {
    foreach (int item in wutCourses)
    {
      #nullable enable
      Enrollment? joinEntity = _db.Enrollments.FirstOrDefault(entry => (entry.CourseId == item && entry.StudentId == studentId));
      #nullable disable

      if(joinEntity == null && studentId != 0)
      {
        Enrollment newEnrollment = new Enrollment();
        newEnrollment.CourseId = item;
        newEnrollment.StudentId = studentId;
        _db.Enrollments.Add(newEnrollment);
        // _db.Enrollments.Add(new Enrollment() {CourseId = item, StudentId = studentId});
        _db.SaveChanges();
      }
    }
    return RedirectToAction("Index");
  }
}
