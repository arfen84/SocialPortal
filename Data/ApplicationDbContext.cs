using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialPortal.Models;

namespace SocialPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Utwor>().ToTable("Utwory");
        }

        public DbSet<Utwor> Utwory { get; set; }
        public DbSet<Zespol> Zespoly { get; set; }
        public DbSet<Autor> Autorzy { get; set; }
        public DbSet<Event> Eventy { get; set; }
        public DbSet<Relacja> Relacje { get; set; }
        public DbSet<Tekst> Teksty { get; set; }
        public DbSet<Album> Albumy { get; set; }
        public DbSet<SocialPortal.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<SocialPortal.Models.artysta> artysta { get; set; }
    }
}
