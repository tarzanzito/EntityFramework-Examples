using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    [Table("View_1")] //or   .ToView("View_1"); in DbContext - OnModelCreating
    [Keyless]  //or   .HasNoKey(); in DbContext - OnModelCreating- because the view not have primary key
    public class MyView1
    {
    //   // public DateTimeOffset? LockoutEnd { get; private set; }
    //   // public bool TwoFactorEnabled { get; private set; }
    //   // public bool PhoneNumberConfirmed { get; private set; }
    //    public string PhoneNumber { get; private set; }
    //   // public string ConcurrencyStamp { get; private set; }
    //   // public string SecurityStamp { get; private set; }
    //   // public string PasswordHash { get; private set; }
    //    public bool EmailConfirmed { get; private set; }
    //    public string NormalizedEmail { get; private set; }
    //    public string Email { get; private set; }
    //    public string NormalizedUserName { get; private set; }
    //    public string UserName { get; private set; }
        public string Id { get; private set; }
    //   // public bool LockoutEnabled { get; private set; }
    //   // public int AccessFailedCount { get; private set; }

        public string UserId { get; private set; }
        public string RoleId { get; private set; }
        public string Name { get; private set; }
        public string NormalizedName { get; private set; }
    }
}

