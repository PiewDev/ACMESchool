namespace Domain.Payments;
public static partial class Errors
{
    public static class Payments
    {
        public static Error InvalidCardToken =>
            Error.Validation("Payment.CardToken", "CardToken is not valid.");
        public static Error StudentNotFound =>
            Error.NotFound("Payment.Student.NotFound", "Student not found.");
        public static Error CourseNotFound =>
            Error.Validation("Payment.Course.NotFound", "Course not found.");
        public static Error CourseFeeNotEquals =>
            Error.Validation("CourseFeeNotEquals", "The provided payment amount does not match the course registration fee.");
        public static Error CourseIsFull =>
            Error.Validation("CourseIsFull", "Course is full.");
    }
}

