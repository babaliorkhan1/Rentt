namespace FinalBackend.Models
{
    public class Year:BaseModel
    {
        public string year { get; set; }    
        public Brand? Brand { get; set; }        
        public int BrandId { get; set; }    
        public List<Car>?  Cars { get; set; }       
    }
}
