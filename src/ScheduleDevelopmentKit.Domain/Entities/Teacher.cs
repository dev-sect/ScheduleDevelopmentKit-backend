using System;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; private set; }
        public PersonName Name { get; set; } = null!;
        public Email Email { get; set; } = null!;
        public PhoneNumber PhoneNumber { get; set; } = null!;

        private Teacher() {}
        public Teacher(Guid id, PersonName name, Email email, PhoneNumber phoneNumber)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}