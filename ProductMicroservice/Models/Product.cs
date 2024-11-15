namespace ProductMicroservice.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Rating { get; set; }
        public long CategoryId { get; set; }
        public long ProviderId {  get; set; }

    }
}
