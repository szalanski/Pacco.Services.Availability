using Pacco.Services.Availability.Core.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Pacco.Services.Availability.Core.Entities
{
    public class AggregateId : IEquatable<AggregateId>
    {
        public Guid Value { get; set; }

        public AggregateId() : this(Guid.NewGuid())
        {
        }

        public AggregateId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidAggregateIdException(value);
            }
            Value = value;
        }

        public bool Equals([AllowNull] AggregateId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((AggregateId)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator AggregateId(Guid value) => new AggregateId(value);

        public static implicit operator Guid(AggregateId value) => value.Value;
        public override string ToString() => Value.ToString();
    }
}
