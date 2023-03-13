using System;
using System.Collections.Generic;

namespace UniversityRegistrar.Models
{
  public class Student
  {
    public int StudentId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfEnrollment { get; set; }
    public Department Major { get; set; }
    public int DepartmentId { get; set; }
    public List<Enrollment> Enrollments { get; set; }
  }
}