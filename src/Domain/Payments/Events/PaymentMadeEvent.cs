using Domain.Courses.ValueObjects;
using Domain.Primitives;
using Domain.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Payments.Events;
    public record PaymentMadeEvent : DomainEvent
    {
        public StudentId StudentId { get; }
        public CourseId CourseId { get; }

        public PaymentMadeEvent(Guid id, StudentId studentId, CourseId courseId): base(id)
        {
            StudentId = studentId;
            CourseId = courseId;
        }
    }
