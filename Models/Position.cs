namespace FinalBackend.Models
{
    public class Position:BaseModel
    {
        public string Name { get; set;}        
        public List<Car>?  Cars { get; set;}
        public List<Brand>? Brands { get; set;}
    }
}
