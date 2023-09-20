using FinalBackend.Models;

namespace FinalBackend.ViewModels
{
    public class PaymentViewModel
    {
        public AppUser? AppUser { get; set; }    
        public string? AppUserId { get; set; }   
       
        public List<Brand>? Brands { get; set; }     
        public Brand? Brand { get; set; }    
        public int? BrandId { get; set; }    
        public List<Model>? Models { get; set; }
        public Model? Model { get; set; }    
        public int? ModelId { get; set; }        
        public List<City>? Cities { get; set; }   
        public City? City { get; set; }
        public int? CityId { get; set; }
        public string? Name { get; set; }    
        public string? Surname { get; set; } 
        public string? Email { get; set; }       
        public string? PhoneNumber { get; set; } 
        public string? Address { get; set; } 
        public DateTime? PickUp { get; set; }  
        public DateTime? Dropoff { get; set; }  
    }
}
