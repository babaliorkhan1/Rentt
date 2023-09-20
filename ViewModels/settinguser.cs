using FinalBackend.Models;

namespace FinalBackend.ViewModels
{
    public class settinguser
    {
        public AppUser? appUser { get; set; }    
        public Setting? setting { get; set; }    
        public Company? company { get; set; }    
        public List<Car>? cars { get; set; } 
        public Car? car { get; set; }    
        public List<Position>? positions { get; set; }   
        public List<Comment> ? comments { get; set; }   
        public Position? position { get; set; }
        public int? PositionId { get; set; }
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }       
        public List<Brand>? Brands { get; set; } 
        public List<Model>?  Models{ get; set; } 
        public Model? Model { get; set; } 
        public int? ModelId { get; set;}
        public List<Year>? Years { get; set; }       
        public int? YearId { get;}
        public Year? Year { get; set; }
        public Comment? Comment { get; set; } 
        public string Email { get; set; }       
 
    }
}
