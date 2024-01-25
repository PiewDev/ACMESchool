using Domain.Primitives;
using Domain.StudentEnrollments;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Students;
public sealed class Student : AggregateRoot
{
    public Student(StudentId id, string name, string lastName, string email,
    PhoneNumber phoneNumber, Address address)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    private Student()
    {

    }

    public StudentId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{Name} {LastName}";
    public string Email { get; private set; } = string.Empty;
    public PhoneNumber PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public bool Active { get; private set; }
    public List<StudentEnrollment> Enrollments { get; private set; }

    public static Student UpdateCustomer(Guid id, string name, string lastName, string email, PhoneNumber phoneNumber, Address address)
    {
        return new Student(new StudentId(id), name, lastName, email, phoneNumber, address);
    }
}
