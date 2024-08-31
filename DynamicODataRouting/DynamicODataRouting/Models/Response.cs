namespace DynamicODataRouting.Models
{
    public class Response
    {
        public IQueryable Content { get; set; }

        public ICollection<string> Metadata { get; set; }
    }
}
