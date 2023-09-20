using System.ComponentModel.DataAnnotations.Schema;

namespace FinalBackend.Models
{
    public class Car:BaseModel
    {
        public string Name { get; set; }    
        public string Year { get; set; }        
        public string Price { get; set; }   
        public string Info1 { get; set; }   
        public string Info2 { get; set; }   
        public string Info3 { get; set; }   
        public Position? Position { get; set; }  
        public int? PositionId { get; set; }
        public Brand? Brand { get; set; }
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }        
        public Model? Model { get; set; }    
        public Year? year { get; set; }  
        public int? YearId { get; set; }     
       
       

        public string PriceInfo1 { get; set; }   
        public string PriceInfo2 { get; set; }   
        public string PriceInfo3 { get; set; }   
        public string ModelName { get; set; }   
        public List<ProductImage>? ProductImages { get; set;}  
        [NotMapped]
        public List<IFormFile>? FormFiles { get; set; }     
        public List<Comment>? Comments { get; set; }
        public bool? Isreservation { get; set; }    


    }
}
