using TIA.BusinessLogicLayerBase.Abstractions;

namespace TIA.BusinessLogicLayer.WebInterface
{
    public interface ITiaModelWebInterface : ITiaModel
    {
        string Token { get; set; }
    }
}
