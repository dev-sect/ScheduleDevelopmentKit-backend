using System.Collections.Generic;

namespace ScheduleDevelopmentKit.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; private set; } = null!;

        private Email() {}
        public Email(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}