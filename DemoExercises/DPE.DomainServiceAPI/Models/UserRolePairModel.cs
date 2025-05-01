namespace DPE.DomainServiceAPI.Models
{
    public class UserRolePairModel
    {
        public required string UserId { get; set; }
        public string? RoleName { get; set; }
    }
}
