namespace Chapter2 {
    public class GuestResponse {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public bool? WillAttend { get; set; }
    }
}