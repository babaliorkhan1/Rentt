namespace FinalBackend.Models
{
    public class Comment:BaseModel
    {
        public string Text { get; set; }
        public string AppUserId { get; set; }       
        public AppUser? AppUser { get; set; }        
        public Car? Car { get; set; }        
        public int CarId { get; set; }      
        public int? dislikecount { get; set; }   
        public int? likecount1 { get; set; }
    }
}
