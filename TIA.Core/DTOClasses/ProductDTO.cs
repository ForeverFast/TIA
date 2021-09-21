using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TIA.Core.DTOClasses
{
    public record ProductDTO : CatalogObjectDTO
    {
        public uint Quantity { get; init; }

        [JsonProperty("Price")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Недопустимый возраст")]
        public uint Price { get; init; }
    }
}
