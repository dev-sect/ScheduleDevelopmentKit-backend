using System.Collections.Generic;

namespace ScheduleDevelopmentKit.Domain.ValueObjects
{
    public class CampusAddress : ValueObject
    {
        public string Value { get; private init; }

        private CampusAddress() { }

        public CampusAddress(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}