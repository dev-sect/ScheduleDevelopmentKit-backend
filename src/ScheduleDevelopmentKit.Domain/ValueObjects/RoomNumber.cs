using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ScheduleDevelopmentKit.Domain.ValueObjects
{
    public class RoomNumber : ValueObject
    {
        public string Value { get; private init; }

        private RoomNumber() { }

        public RoomNumber(string value)
        {
            if (!Regex.IsMatch(value, @"^[0-9]{0,10}[a-zA-Z]{0,1}$"))
                throw new ArgumentException("Wrong room number format");
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}