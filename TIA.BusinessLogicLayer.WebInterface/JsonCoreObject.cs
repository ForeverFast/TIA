using System.Collections.Generic;

namespace TIA.BusinessLogicLayer.WebInterface
{
    public class JsonCoreObject<T>
    {
        public T Object { get; set; }

        public List<string> Errors { get; set; }
    }
}
