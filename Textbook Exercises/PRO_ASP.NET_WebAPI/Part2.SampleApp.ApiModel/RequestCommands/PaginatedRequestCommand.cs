namespace PingYourPackage.ApiModel
{
    public class PaginatedRequestCommand : IRequestCommand
    {
        [Minimum(1)]
        public int Page { get; set; }
        [Minimum(1), Maximum(1)]
        public int Take { get; set; }
    }
}
