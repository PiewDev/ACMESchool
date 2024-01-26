using Domain.Courses.ValueObjects;
using Domain.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.StudentEnrollments
{
    public interface IEnrollmentService
    {
        Task<ErrorOr<Guid>> EnrollStudentInCourse(StudentId studentId, CourseId courseId);

    }
}
