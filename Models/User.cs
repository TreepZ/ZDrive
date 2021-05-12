﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ZDrive.Models
{
    public partial class User
    {
        public User()
        {
            Cars = new HashSet<Car>();
            ReservedSeats = new HashSet<ReservedSeat>();
            Routes = new HashSet<Route>();
        }

        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserType { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string UserEmail { get; set; }
        public string AspUserId { get; set; }
        [ForeignKey(nameof(AspUserId))]
        public virtual IdentityUser AspUser { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        [InverseProperty(nameof(ReservedSeat.User))]
        public virtual ICollection<ReservedSeat> ReservedSeats { get; set; }
        [InverseProperty(nameof(Route.User))]
        public virtual ICollection<Route> Routes { get; set; }
        
}
}