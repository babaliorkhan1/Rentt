﻿using Microsoft.AspNetCore.Identity;

namespace FinalBackend.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string? Image { get; set; }       
        public List<Comment> Comments { get; set; }     
        public List<Favourite>  Favourites { get; set; }     
        public List<Reservationn> reservationns { get; set; }
      
    }
}
