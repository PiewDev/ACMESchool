namespace Domain.Courses;
public static partial class Errors
{
    public static class Courses
    {
        public static Error MaxStudentsInvalidFormat =>
            Error.Validation("Courses.MaxStudents", "MaxStudents has not valid format.");

        public static Error CourseDurationInvalid =>
            Error.Validation("Courses.CourseDuration", "CourseDuration is not valid.");
    }
}
