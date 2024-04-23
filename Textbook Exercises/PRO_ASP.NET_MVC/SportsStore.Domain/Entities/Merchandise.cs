namespace SportsStore.Domain
{

    public class Merchandise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category {  get; set; }
        public decimal Price { get; set; }
        public bool IsValid { get; set; }
    }
}