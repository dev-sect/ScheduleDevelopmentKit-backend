using System.Collections.Generic;

namespace ScheduleDevelopmentKit.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        
        public string Value { get; private init; } = null!;

        private PhoneNumber() {}
        public PhoneNumber(string value)
        {
            Value = value;
        }
        
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}