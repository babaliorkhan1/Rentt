using FinalBackend.Models;

namespace FinalBackend.ViewModels
{
    public class CarViewModel
    {
        public AppUser? appUser { get; set; }
        public Setting? setting { get; set; }
        public List<Car>? cars { get; set; }
        public Car? car { get; set; }
        public List<Position>? positions { get; set; }
        public Position? position { get; set; }
        public int? PositionId { get; set; }
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public List<Brand>? Brands { get; set; }
        public Model? Model { get; set; }    
        public int? ModelId { get; set; }    
        public List<Model>? Models { get; set; } 
        public List<Year>? Years { get; set; }       
        public int? YearId { get; set; }     
        public Year? Year { get; set; }      
        
    }
}
