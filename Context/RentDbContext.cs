using FinalBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalBackend.Context
{
    public class RentDbContext: IdentityDbContext<AppUser>
    {

        public DbSet<Setting> settings { get; set; }    
        public DbSet<Company> companies { get; set; }    
        public DbSet<Car>  Cars { get; set; }    
        public DbSet<Position>  Positions { get; set; }    
        public DbSet<ProductImage>  ProductImages { get; set; }    
        public DbSet<Comment>   Comments{ get; set; }    
        public DbSet<Brand>  Brands{ get; set; }    
        public DbSet<Model>  Models{ get; set; }    
        public DbSet<Year>   Years{ get; set; }    
        public DbSet<Favourite>   Favourites{ get; set; }    
        public DbSet<City>   Cities{ get; set; }    
        public DbSet<Reservationn>   reservationOrders{ get; set; }    
        public DbSet<Subscribe>   Subscribes{ get; set; }    
        
        public RentDbContext(DbContextOptions<RentDbContext> options):base(options)
        {

        }
    }



   
}
