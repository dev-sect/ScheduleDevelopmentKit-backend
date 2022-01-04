using System;
using System.Collections.Generic;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class Campus
    {
        private List<Room> _rooms;
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
    }
}