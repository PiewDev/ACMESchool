using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Primitives;
using Domain.StudentEnrollments;
using Domain.Students;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Errors = Domain.StudentEnrollments.Errors;

namespace Application.Enrollments
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository; 
        private readonly IUnitOfWork _unitOfWork;


        public EnrollmentService(IStudentRepository studentRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> EnrollStudentInCourse(StudentId studentId, CourseId courseId)
        {
            if (await _studentRepository.GetByIdAsync(studentId) is not Student updatedStudent){
                return Errors.StudentEnrollment.StudentNotFound;
            }
            if (await _courseRepository.GetByIdAsync(courseId) is not Course updatedCourse)
            {
                return Errors.StudentEnrollment.CourseNotFound;
            }

            ErrorOr<StudentEnrollment> enrollment = updatedStudent.EnrollInCourse(updatedCourse);
            
            if (enrollment.IsError)
            {
                return enrollment.Errors;
            }

            enrollment = updatedCourse.AddEnrollment(enrollment.Value);

            if (enrollment.IsError)
            {
                return enrollment.Errors;
            }

            await _unitOfWork.SaveChangesAsync();

            return enrollment.Value.Id.Value;
        }
    }
}
