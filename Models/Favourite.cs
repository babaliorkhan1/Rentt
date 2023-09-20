namespace FinalBackend.Models
{
    public class Favourite:BaseModel
    {
        public AppUser AppUser { get; set; }    
        public string AppUserId { get; set; }  
        public Car Car { get; set; }    
        public int CarId { get; set; }

    }
}
