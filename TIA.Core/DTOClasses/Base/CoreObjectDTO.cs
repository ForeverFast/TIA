using System;

namespace TIA.Core.DTOClasses
{
    public abstract record CoreObjectDTO 
    {
        public Guid Id { get; init; }
    }
}
