using System;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; private set; }
        public RoomNumber Number { get; private set; }
        public bool BelongsToFaculty { get; private set; }

        private Room() { }

        public Room(Guid id, RoomNumber number, bool belongsToFaculty)
        {
            Id = id;
            Number = number;
            BelongsToFaculty = belongsToFaculty;
        }
    }
}