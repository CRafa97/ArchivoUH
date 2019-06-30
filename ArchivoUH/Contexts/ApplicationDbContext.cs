using ArchivoUH.Domain;
using ArchivoUH.Domain.Auth;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace ArchivoUH.Contexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<User> AppUsers { get; set; }

        public DbSet<Graduated> Graduates { get; set; }
        public DbSet<Leaved> Leaves { get; set; }
        public DbSet<Administrative> Administratives { get; set; }


        public DbSet<KeyWord> KeyWords { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        public ApplicationDbContext()
            : base("ArchivoUH")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.UserId, r.RoleId }).ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId }).ToTable("AspNetUserLogins");

            modelBuilder.Entity<Graduated>().HasRequired<Course>(g => g.Course).WithMany(c => c.Graduates);
            modelBuilder.Entity<Leaved>().HasRequired<Course>(l => l.Course).WithMany(c => c.Leaves);
            modelBuilder.Entity<Graduated>().HasRequired<Faculty>(g => g.Faculty).WithMany(f => f.Graduates);
            modelBuilder.Entity<Leaved>().HasRequired<Faculty>(l => l.Faculty).WithMany(f => f.Leaves);
            modelBuilder.Entity<Graduated>().HasRequired<Locality>(g => g.Locality).WithMany(l => l.Graduates);
            modelBuilder.Entity<Administrative>().HasRequired<KeyWord>(a => a.KeyWord).WithMany(kw => kw.Administratives);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}