using System.Collections.Generic;

namespace UniversityRegistrar.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public Department Department { get; set; }
    public int DepartmentId { get; set; }
    public List<Enrollment> Enrollments { get; set; }
  }
}