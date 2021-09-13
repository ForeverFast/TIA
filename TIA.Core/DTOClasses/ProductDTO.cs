namespace TIA.Core.DTOClasses
{
    public record ProductDTO : CatalogObjectDTO
    {
        public uint Quantity { get; init; }
        public uint Price { get; init; }
    }
}
