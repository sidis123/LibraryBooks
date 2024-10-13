using Microsoft.EntityFrameworkCore;
using LibraryBE.Models;

namespace LibraryBE.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)  
                .WithMany(u => u.Reservations) 
                .HasForeignKey(r => r.id_User); 

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)   
                .WithMany(b => b.Reservations) 
                .HasForeignKey(r => r.id_Book); 
        }


    }
}
