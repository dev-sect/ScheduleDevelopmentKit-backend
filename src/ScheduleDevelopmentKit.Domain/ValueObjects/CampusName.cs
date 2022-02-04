using System.Collections.Generic;

namespace ScheduleDevelopmentKit.Domain.ValueObjects
{
    public class CampusName : ValueObject
    {
        public string Value { get; private init; }

        private CampusName() {}

        public CampusName(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}