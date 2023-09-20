namespace FinalBackend.Models
{
    public class City:BaseModel 
    {
        public string Name { get; set; }
        public List<Reservationn> Reservationns { get; set; }
    }
}
