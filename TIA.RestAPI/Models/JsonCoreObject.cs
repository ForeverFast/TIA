using System.Collections.Generic;

namespace TIA.RestAPI.Models
{
    public class JsonCoreObject<T>
    {
        public T Object { get; set; }

        public Dictionary<string, string> Errors { get; set; }
    }
}
