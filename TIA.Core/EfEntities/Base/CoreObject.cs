using System;
using System.ComponentModel.DataAnnotations;

namespace TIA.Core.EfEntities
{
    public abstract class CoreObject 
    {
        [Key]
        public Guid Id { get; set; }
    }
}
