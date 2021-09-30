using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TIA.Core.DTOClasses
{
    public record ProductDTO : CatalogObjectDTO
    {
        public DateTime? SomeDate { get; set; }

        public uint Quantity { get; init; }

        [JsonProperty("Price")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Недопустимое значение")]
        public uint Price { get; init; }
    }
}
