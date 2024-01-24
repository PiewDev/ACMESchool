using ErrorOr;

namespace Domain.Students;


public static partial class Errors
{
    public static class Student
    {
        public static Error PhoneNumberWithBadFormat =>
            Error.Validation("Student.PhoneNumber", "Phone number has not valid format.");

        public static Error AddressWithBadFormat =>
            Error.Validation("Student.Address", "Address is not valid.");
    }

}
