using System;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; private set; }
        public Campus Campus { get; private set; }
        public RoomNumber Number { get; set; }
        public bool BelongsToFaculty { get; set; }

        private Room() { }

        public Room(Guid id, Campus campus, RoomNumber number, bool belongsToFaculty)
        {
            Id = id;
            Campus = campus;
            Number = number;
            BelongsToFaculty = belongsToFaculty;
        }
    }
}