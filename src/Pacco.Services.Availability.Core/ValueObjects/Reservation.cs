using System;
using System.Collections.Generic;

namespace Pacco.Services.Availability.Core.ValueObjects
{
    public class Reservation : ValueObject
    {
        public DateTime DateTime { get; }
        public int Priority { get; }

        public Reservation(DateTime dateTime, int priority)
        {
            DateTime = dateTime;
            Priority = priority;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return DateTime; 
            yield return Priority;
        }
    }
}
