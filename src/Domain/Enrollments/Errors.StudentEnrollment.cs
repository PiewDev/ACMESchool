using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enrollments;
public static partial class Errors
{
    public static class StudentEnrollment
    {
        public static Error StudentNotFound =>
            Error.NotFound("StudentEnrollment.Student.NotFound", "Student not found.");
        public static Error CourseNotFound =>
            Error.Validation("StudentEnrollment.Course.NotFound", "Course not found.");
        public static Error CourseIsFull =>
            Error.Validation("CourseIsFull", "Course is full.");
    }
}
