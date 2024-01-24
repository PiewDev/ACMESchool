using Domain.Courses.ValueObjects;
using Domain.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Courses;
public class StudentEnrollment
{
    public StudentEnrollmentId Id { get; private set; }
    public StudentId StudentId { get; private set; }
    public CourseId CourseId { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    public StudentEnrollment(StudentId studentId, CourseId courseId)
    {
        Id = new StudentEnrollmentId(Guid.NewGuid());
        StudentId = studentId;
        CourseId = courseId;
        EnrollmentDate = DateTime.UtcNow;
    }

}