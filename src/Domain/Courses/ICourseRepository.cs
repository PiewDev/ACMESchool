using Domain.Courses.ValueObjects;
using Domain.Students;

namespace Domain.Courses;
public interface ICourseRepository
{
    void Add(Course course);
    Task<Course> GetByIdAsync(CourseId id);
}