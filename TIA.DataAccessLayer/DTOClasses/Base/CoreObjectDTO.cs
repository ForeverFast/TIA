using System;

namespace TIA.DataAccessLayer.DTOClasses
{
    public abstract record CoreObjectDTO 
    {
        public Guid Id { get; init; }
    }
}
