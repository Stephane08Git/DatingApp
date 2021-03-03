using API.Entities;

namespace API.DTOs
{
    public class PhotoForApprouvalDTo
    {
        public int Id { get; set; }
        public bool IsValid { get; set; }
        public bool IsMain { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
    }
}