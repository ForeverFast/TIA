namespace TIA.RestAPI.Models
{
    public class JsonCoreObject<T>
    {
        public T Object { get; set; }

        public string error { get; set; }
    }
}
