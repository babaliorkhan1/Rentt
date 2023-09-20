namespace FinalBackend.Models
{
    public class ProductImage:BaseModel
    {
        public bool IsMain { get; set;}
        public string Image { get; set;}
        public int CarId { get; set;}
        public Car Car { get; set;}
    }
}
