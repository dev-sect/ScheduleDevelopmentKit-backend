using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ScheduleDevelopmentKit.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; private init; } = null!;

        private PhoneNumber() {}
        public PhoneNumber(string value)
        {
            if (!Regex.IsMatch(value, @"^\+7[0-9]{10}$"))
                throw new ArgumentException("Wrong phone number format");
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}