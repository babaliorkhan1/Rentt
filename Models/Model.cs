namespace FinalBackend.Models
{
    public class Model:BaseModel
    {
        public string Name { get; set;}    
        public Brand? Brand { get; set;}    
        public int BrandId { get; set;}    
        public List<Car>?  Cars { get; set;}
        public List<Reservationn> Reservationns { get; set; }
    }
}
