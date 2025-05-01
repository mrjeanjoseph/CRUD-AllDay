namespace DPE.DomainServiceAPI.Models
{
    public class ApplicationUserModel
    {
        public required string Id { get; set; }
        public string? Email { get; set; }

        public Dictionary<string, string> Roles { get; set; } = [];
    }
}
