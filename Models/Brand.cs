namespace FinalBackend.Models
{
    public class Brand:BaseModel
    {
        public string Name { get; set; }    
        public Position? Position { get; set; }   
        public int PositionId { get; set; }     
        public List<Car>? Cars { get; set; } 
        public List<Model>? Models { get; set; } 
        public List<Year>?  Years { get; set; }
        public List<Reservationn>? Reservationns { get; set; }

    }
}
