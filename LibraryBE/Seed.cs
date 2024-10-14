using LibraryBE.Data;
using LibraryBE.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryBE
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            
            if (!dataContext.Books.Any())
            {
                // Create sample users
                var users = new List<User>()
                {
                    new User { Name = "John Doe", Email = "john.doe@example.com", Password = "password123" },
                    new User { Name = "Jane Smith", Email = "jane.smith@example.com", Password = "password123" },
                    new User { Name = "Emily Johnson", Email = "emily.johnson@example.com", Password = "password123" }
                };

                // Create sample books (including audiobooks)
                var books = new List<Book>()
                {
                    new Book { Name = "To Kill a Mockingbird", Year = 1960, Type = "Book", Picture = "https://upload.wikimedia.org/wikipedia/commons/4/4f/To_Kill_a_Mockingbird_%28first_edition_cover%29.jpg" },
                    new Book { Name = "1984", Year = 1949, Type = "Book", Picture = "https://s3.amazonaws.com/adg-bucket/1984-george-orwell/3423-medium.jpg" },
                    new Book { Name = "The Great Gatsby", Year = 1925, Type = "Audiobook", Picture = "https://upload.wikimedia.org/wikipedia/commons/7/7a/The_Great_Gatsby_Cover_1925_Retouched.jpg" },
                    new Book { Name = "The Catcher in the Rye", Year = 1951, Type = "Book", Picture = "https://m.media-amazon.com/images/I/8125BDk3l9L._AC_UF1000,1000_QL80_.jpg" }
                };

                // Create sample reservations with total cost hardcoded as 100
                var reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        User = users[0],  // John Doe
                        Book = books[0],  // To Kill a Mockingbird
                        CreationDate = DateTime.Now.AddDays(-5),
                        Days = 4,
                        QuickPickup = false,
                        TotalCost = 100  
                    },
                    new Reservation
                    {
                        User = users[1],  // Jane Smith
                        Book = books[2],  // The Great Gatsby (Audiobook)
                        CreationDate = DateTime.Now.AddDays(-3),
                        Days = 7,
                        QuickPickup = true,
                        TotalCost = 100  
                    },
                    new Reservation
                    {
                        User = users[2],  // Emily Johnson
                        Book = books[3],  // The Catcher in the Rye
                        CreationDate = DateTime.Now.AddDays(-1),
                        Days = 2,
                        QuickPickup = false,
                        TotalCost = 100  
                    }
                };

                
                dataContext.Users.AddRange(users);

               
                dataContext.Books.AddRange(books);

                
                dataContext.Reservations.AddRange(reservations);

                
                dataContext.SaveChanges();
            }
        }
    }
}
