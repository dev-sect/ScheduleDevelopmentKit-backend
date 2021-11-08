using System;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; private set; }
        public PersonName Name { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;
        
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