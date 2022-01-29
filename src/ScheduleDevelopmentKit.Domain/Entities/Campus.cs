using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleDevelopmentKit.Common.Exceptions;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class Campus
    {
        private readonly List<Room> _rooms = new List<Room>();
        public Guid Id { get; private set; }
        public CampusName Name { get; private set; }
        public CampusAddress Address { get; private set; }
        public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

        private Campus() { }

        public Campus(Guid id, CampusName name, CampusAddress address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public void AddRoom(Room room)
        {
            if (_rooms.Any(r => r.Id == room.Id))
                throw new RoomAlreadyAddedException($"Room with id {room.Id} already added to campus {Id}");
            _rooms.Add(room);
        }
    }
}