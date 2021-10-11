using TIA.BusinessLogicLayerBase;

namespace TIA.BusinessLogicLayer.WebInterface
{
    public partial class TiaModel : TiaModelBase, ITiaModelWebInterface
    {
        private string _token;

        public string Token { get => $"Bearer {_token}"; set => _token = value; }

        public TiaModel()
        {

        }
    }
}
